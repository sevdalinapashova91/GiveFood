using GiveFood.DAL.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GiveFoodInfrastructure.Tasks
{
    public class SendNotificationTask : ISendNotificationTask
    {
        private readonly INotificationRepository notificationRepository;
        private readonly INotificationHub notificationHub;

        public SendNotificationTask(
            INotificationRepository notificationRepository, 
            INotificationHub notificationHub)
        {
            this.notificationRepository = notificationRepository;
            this.notificationHub = notificationHub;
        }

        public async Task Send(Guid userId)
        {
            var unreadNotificationCount = notificationRepository
                .FilterByUser(userId)
                .Where(x=>!x.IsRead)
                .Count();

            await notificationHub.SendNotification(userId.ToString(), unreadNotificationCount);
        }
    }
}
