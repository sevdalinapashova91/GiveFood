using System.Threading.Tasks;
using GiveFoodServices.Users.Models;

namespace GiveFoodServices.Users
{
    public interface IAuthService
    {
        Task RegisterAsync(UserDto userDto);
    }
}