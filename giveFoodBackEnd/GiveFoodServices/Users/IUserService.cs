using System.Collections.Generic;
using System.Threading.Tasks;
using GiveFoodServices.Users.Models;

namespace GiveFoodServices.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserInRoleDto>> GetGiversAsync();
        Task<IEnumerable<UserInRoleDto>> GetTakersAsync();
    }
}