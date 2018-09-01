using System.Threading.Tasks;
using GiveFoodServices.Users.Models;

namespace GiveFoodServices.Users
{
    public interface IAuthService
    {
        Task LoginAsync(LoginDto loginDto);

        Task LogoutAsync();

        Task RegisterAsync(UserDto userDto);
    }
}