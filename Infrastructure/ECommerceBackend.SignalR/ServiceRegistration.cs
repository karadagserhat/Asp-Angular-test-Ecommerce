using ECommerceBackend.Application.Abstractions.Hubs;
using ECommerceBackend.SignalR.HubsServices;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceBackend.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IProductHubService, ProductHubService>();
            collection.AddSignalR();
        }
    }
}
