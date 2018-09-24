using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GiveFoodInfrastructure
{
    public class NotificationHub : Hub, INotificationHub
    {
        private readonly IHubContext<NotificationHub> context;

        public NotificationHub(IHubContext<NotificationHub> context)
        {
            this.context = context;
        }
        public async Task SendNotification(string user, int unreadNotification)
        {
            await context.Clients.All.SendAsync("ReceiveMessage",
                new { UserId = user, NotificationCount = unreadNotification });
        }
    }
}
