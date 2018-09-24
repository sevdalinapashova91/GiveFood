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

        public Task CreateAsync(Guid sendTo, Guid creator, MessageType type, string creatorName)
        {
            dbContext.Add(
                new Notification
                {
                    SendTo = sendTo,
                    Creator = creator,
                    IsRead = false,
                    MessageType = type,
                    DateCreated = DateTime.UtcNow,
                    CreatorName = creatorName,
                });

            return dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Notification notification)
        {
            dbContext.Notifications.Remove(notification);

            return dbContext.SaveChangesAsync();
        }

        public IQueryable<Notification> FilterByUser(Guid userId) =>
            dbContext.Notifications.Where(x => x.SendTo == userId);

        public IQueryable<Notification> FilterUnread(Guid userId) =>
               dbContext.Notifications.Where(x => x.SendTo == userId && !x.IsRead);


        public Task<Notification> Get(long id) =>
            dbContext.Notifications.FindAsync(id);


        public Task UpdateAsync(Notification notification, bool isRead)
        {
            notification.IsRead = isRead;

            return dbContext.SaveChangesAsync();
        }
    }
}
