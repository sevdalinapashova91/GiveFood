using GiveFood.DAL.Notifications;
using GiveFoodDataModels;
using GiveFoodInfrastructure.BackgroudTask;
using GiveFoodInfrastructure.Tasks;
using GiveFoodServices.Users.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveFoodServices.Users
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository notificationRepository;
        private readonly UserManager<User> userManager;
        private readonly IBackgroundTask backgroundTask;

        public NotificationService(INotificationRepository notificationRepository, UserManager<User> userManager, 
            IBackgroundTask backgroundTask)
        {
            this.notificationRepository = notificationRepository;
            this.userManager = userManager;
            this.backgroundTask = backgroundTask;
        }

        public IEnumerable<NotificationDto> GetAllNotification(Guid userId)
        {
            return notificationRepository
                .FilterByUser(userId)
                .OrderByDescending(x => x.Id)
                .Select(x => new NotificationDto
                {
                    Id = x.Id,
                    DateCreated = x.DateCreated,
                    MessageType = x.MessageType,
                    IsRead = x.IsRead,
                    SenderName = x.CreatorName
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
            backgroundTask.EnqueueOneTimeJob<ISendNotificationTask>(x => x.Send(notification.SendTo));
        }

        public async Task DeleteAsync(long id)
        {
            var notification = await notificationRepository.Get(id);
            await notificationRepository.DeleteAsync(notification);
        }

        public IEnumerable<NotificationDto> GetUnread(Guid userId) =>
           notificationRepository
                .FilterUnread(userId)
                .OrderByDescending(x => x.Id)
               .Select(x => new NotificationDto
               {
                   Id = x.Id,
                   DateCreated = x.DateCreated,
                   MessageType = x.MessageType,
                   IsRead = x.IsRead,
                   SenderName = x.CreatorName
               })
                .ToList();
    }
}
