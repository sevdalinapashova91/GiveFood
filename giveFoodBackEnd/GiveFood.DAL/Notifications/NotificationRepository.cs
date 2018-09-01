using System;
using System.Linq;
using GiveFoodDataModels;
using GiveFoodData;
using System.Threading.Tasks;

namespace GiveFood.DAL.Notifications
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly GiveFoodDbContext dbContext;

        public NotificationRepository(GiveFoodDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task CreateAsync(Guid sendTo, string message)
        {
            dbContext.Add(
                new Notification
                {
                    SendTo = sendTo,
                    IsRead = false,
                    Message = message,
                    DateCreated = DateTime.UtcNow
                });

            return dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Notification notification)
        {
            dbContext.Notifications.Remove(notification);

            return dbContext.SaveChangesAsync();
        }

        public IQueryable<Notification> FilterByUser(Guid userId)
        {
            return dbContext.Notifications.Where(x => x.SendTo == userId);
        }

        public Task<Notification> Get(long id)
        {
            return dbContext.Notifications.FindAsync(id);
        }

        public Task UpdateAsync(Notification notification, bool isRead)
        {
            notification.IsRead = isRead;

            return dbContext.SaveChangesAsync();
        }
    }
}
