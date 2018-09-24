using GiveFoodDataModels;

namespace GiveFoodServices.Users
{
    public class RequestApprovalMessageFactory : IRequestApprovalMessageFactory
    {
       public MessageType Create(UserType userType)
       {
            MessageType message = MessageType.Invalid;

            switch (userType)
            {
                case UserType.Giver:
                    {
                        message = MessageType.GiverApprovalPending;
                        break;
                    }
                case UserType.Taker:
                    {
                        message = MessageType.TakerApprovalPending;
                        break;
                    }
                case UserType.Admin:
                    {
                        message = MessageType.AdminApprovalPending;
                        break;
                    }

            }

            return message;
       }
    }
}
