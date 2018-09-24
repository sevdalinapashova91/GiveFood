using GiveFood.DAL.Notifications;
using GiveFoodDataModels;
using GiveFoodInfrastructure.BackgroudTask;
using GiveFoodInfrastructure.Tasks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GiveFoodServices.Users
{
    public class ApprovalService : IApprovalService
    {
        private readonly UserManager<User> userManager;
        private readonly INotificationRepository notificationRepository;
        private readonly IRequestApprovalMessageFactory requestApprovalMessageFactory;
        private readonly IBackgroundTask backgroundTask;

        public ApprovalService(
            UserManager<User> userManager,
            INotificationRepository notificationRepository,
            IRequestApprovalMessageFactory requestApprovalMessageFactory,
            IBackgroundTask background)
        {
            this.userManager = userManager;
            this.notificationRepository = notificationRepository;
            this.requestApprovalMessageFactory = requestApprovalMessageFactory;
            this.backgroundTask = background;
        }

        public async Task RequestApproval(string email, UserType userType, Guid creator) =>
            await CreateNotification((await userManager.FindByEmailAsync(email)).Id, 
                 creator, requestApprovalMessageFactory.Create(userType));
        
        public async Task Reject(long id, Guid creator) =>
            await CreateNotification((await notificationRepository.Get(id)).Creator, creator, MessageType.Rejected);
        

        public async Task Approve(long id, Guid creator) =>
           await CreateNotification((await notificationRepository.Get(id)).Creator, creator, MessageType.Approved);
        
      
        private async Task CreateNotification(Guid sendTo, Guid creator, MessageType type)
        {
            var userCreator = await userManager.FindByIdAsync(creator.ToString());

            await notificationRepository.CreateAsync(sendTo, creator, type, userCreator.Name);

            backgroundTask.EnqueueOneTimeJob<ISendNotificationTask>(x => x.Send(sendTo));
        }
    }
}
