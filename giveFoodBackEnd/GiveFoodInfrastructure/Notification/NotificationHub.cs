using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GiveFoodInfrastructure
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string user, int unreadNotification)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, unreadNotification);
        }
    }
}
