using ECommerceBackend.Application.Abstractions.Hubs;
using ECommerceBackend.SignalR.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace ECommerceBackend.SignalR.HubsServices
{
    public class ProductHubService(IHubContext<ProductHub> hubContext, IHttpContextAccessor httpContextAccessor) : IProductHubService
    {
        private readonly IHubContext<ProductHub> _hubContext = hubContext;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public async Task ProductAddedMessageAsync(string message)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            if (currentUser != null)
            {
                var connectionId = ProductHub.GetConnectionId(currentUser);

                if (connectionId != null)
                {
                    await _hubContext.Clients.AllExcept(connectionId)
                        .SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
                }
            }
        }
    }
}