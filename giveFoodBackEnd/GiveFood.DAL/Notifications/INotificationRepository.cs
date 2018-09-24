using GiveFoodDataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GiveFood.DAL.Notifications
{
    public interface INotificationRepository
    {
        Task<Notification> Get(long id);

        IQueryable<Notification> FilterByUser(Guid userId);

        IQueryable<Notification> FilterUnread(Guid userId);

        Task CreateAsync(Guid sendTo, Guid creator, MessageType type, string creatorName);

        Task UpdateAsync(Notification notification, bool isRead);

        Task DeleteAsync(Notification notification);
    }
}
