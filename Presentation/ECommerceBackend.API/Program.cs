using System.Collections.ObjectModel;
using ECommerceAPI.Infrastructure;
using ECommerceBackend.API.Configurations.ColumnWriters;
using ECommerceBackend.API.Extensions;
using ECommerceBackend.Application;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Infrastructure.Helpers;
using ECommerceBackend.Persistence;
using ECommerceBackend.Persistence.Contexts;
using ECommerceBackend.Persistence.Services;
using ECommerceBackend.SignalR;
using Infrastructure.Data;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddSignalRServices();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

var columnOptions = new ColumnOptions
{
  AdditionalColumns = new Collection<SqlColumn>
    {
        new SqlColumn
        {
            ColumnName = "UserName",
            DataType = System.Data.SqlDbType.NVarChar,
            DataLength = 50,
            AllowNull = true,
        }
    }
};

// Properties'i kaldırmak yerine, sadece gerekli kolonları etkinleştirelim
columnOptions.Store.Remove(StandardColumn.Properties);
columnOptions.Store.Add(StandardColumn.LogEvent);

Logger log = new LoggerConfiguration()
  .WriteTo.Console()
  .WriteTo.File("logs/log.txt")
 .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
           sinkOptions: new MSSqlServerSinkOptions
           {
             TableName = "logs",
             AutoCreateSqlTable = true
           },
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
        columnOptions: columnOptions)
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"] ?? "http://localhost:5341")
    .Enrich.FromLogContext()
    .Enrich.With<CustomUserNameColumn>()
    .MinimumLevel.Information()
  .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
  logging.LoggingFields = HttpLoggingFields.All;
  logging.RequestHeaders.Add("sec-ch-ua");
  logging.MediaTypeOptions.AddText("application/javascript");
  logging.RequestBodyLogLimit = 4096;
  logging.ResponseBodyLogLimit = 4096;
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
    });

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>(options =>
            {
              options.Password.RequiredLength = 3;
              options.Password.RequireNonAlphanumeric = false;
              options.Password.RequireDigit = false;
              options.Password.RequireLowercase = false;
              options.Password.RequireUppercase = false;
            })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ECommerceBackendDbContext>();

var app = builder.Build();

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseSerilogRequestLogging();
app.UseHttpLogging();

// Configure the HTTP request pipeline.
app.UseCors();

app.UseAuthorization();

app.Use(async (context, next) =>
{
  var username = context.User?.Identity?.IsAuthenticated == true
      ? context.User.Identity.Name
      : "Anonymous";

  using (LogContext.PushProperty("UserName", username))
  {
    await next();
  }
});

app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>(); // api/login

app.MapHubs();

try
{
  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;
  var context = services.GetRequiredService<ECommerceBackendDbContext>();
  var userManager = services.GetRequiredService<UserManager<AppUser>>();
  await context.Database.MigrateAsync();
  await StoreContextSeed.SeedAsync(context, userManager);
}
catch (Exception ex)
{
  Console.WriteLine(ex);
  throw;
}

app.Run();
