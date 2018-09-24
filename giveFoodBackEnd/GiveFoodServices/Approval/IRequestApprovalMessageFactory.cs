using GiveFoodDataModels;

namespace GiveFoodServices.Users
{
    public interface IRequestApprovalMessageFactory
    {
        MessageType Create(UserType userType);
    }
}