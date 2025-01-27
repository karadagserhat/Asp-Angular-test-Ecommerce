using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ECommerceBackend.SignalR.Hubs
{
    public class ProductHub : Hub
    {
        // Her bağlantı için kullanıcı-bağlantı eşleşmesini tutacağız
        private static readonly Dictionary<string, string> UserConnections = new Dictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            var userName = Context?.User?.Identity?.Name;

            if (userName != null)
            {
                UserConnections[userName] = Context!.ConnectionId;
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userName = Context?.User?.Identity?.Name;

            if (userName != null)
            {
                UserConnections.Remove(userName);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public static string? GetConnectionId(string? userName)
        {
            if (userName == null) return null;

            return UserConnections.TryGetValue(userName, out string? connectionId) ? connectionId : null;
        }

    }
}
