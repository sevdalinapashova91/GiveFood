using System.Threading.Tasks;
using GiveFoodDataModels;
using System;

namespace GiveFoodServices.Users
{
    public interface IApprovalService
    {
        Task Reject(long notificationId, Guid creator);

        Task RequestApproval(string email, UserType userType, Guid creator);

        Task Approve(long notificationId, Guid creator);
    }
}