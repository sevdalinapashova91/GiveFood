using System.Threading.Tasks;

namespace GiveFoodInfrastructure
{
    public interface INotificationHub
    {
        Task SendNotification(string user, int unreadNotification);
    }
}