using System.Threading.Tasks;

namespace GiveFoodWebApi.Authorization
{
    public interface IAuthorizeService
    {
        Task<bool> IsAuthorized();
    }
}