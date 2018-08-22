using System.Threading.Tasks;
using GiveFoodServices.Users.Models;

namespace GiveFoodServices.Users
{
    public interface IAuthService
    {
        Task Login(LoginDto loginDto);
        Task Logout();
        Task Register(UserDto userDto);
    }
}