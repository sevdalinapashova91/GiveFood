using GiveFood.DAL.Notifications;
using GiveFoodDataModels;
using GiveFoodServices.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveFoodServices.Users
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public IEnumerable<NotificationDto> GetAllNotification(Guid userId)
        {
            return notificationRepository
                .FilterByUser(userId)
                .OrderByDescending(x=>x.Id)
                .Select(x=> new NotificationDto
                {
                    Id=x.Id,
                    DateCreated =x.DateCreated,
                    Message = x.Message,
                    IsRead = x.IsRead
                })
                .ToList();
        }

        public Task<Notification> GetAsync(long id)
        {
            return notificationRepository.Get(id);
        }

        public async Task UpdateReadAsync(long id)
        {
            var notification = await notificationRepository.Get(id);
            await notificationRepository.UpdateAsync(notification, true);
        }

        public async Task DeleteAsync(long id)
        {
            var notification = await notificationRepository.Get(id);
            await notificationRepository.DeleteAsync(notification);
        }
    }
}
