using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GiveFoodDataModels;
using GiveFoodServices.Users.Models;

namespace GiveFoodServices.Users
{
    public interface INotificationService
    {
        Task DeleteAsync(long id);

        IEnumerable<NotificationDto> GetAllNotification(Guid userId);

        Task<Notification> GetAsync(long id);

        Task UpdateReadAsync(long id);
    }
}