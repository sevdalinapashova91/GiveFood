using System;
using System.Threading.Tasks;

namespace GiveFoodInfrastructure.Tasks
{
    public interface ISendNotificationTask
    {
        Task Send(Guid userId);
    }
}