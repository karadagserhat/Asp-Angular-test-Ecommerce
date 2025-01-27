using ECommerceBackend.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace ECommerceBackend.API.Extensions
{
    static public class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var exception = contextFeature.Error;

                        // Check if the exception is of type AuthenticationErrorException
                        if (exception is AuthenticationErrorException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // Return 400 for authentication error
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Default to 500
                        }

                        context.Response.ContentType = MediaTypeNames.Application.Json;

                        logger.LogError(contextFeature.Error.Message);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Title = "Hata alındı!"
                        })); ;
                    }
                });
            });
        }
    }
}
