using System.Threading.Tasks;
using GiveFoodServices.Admin.Models;
using System.Collections.Generic;
using System;

namespace GiveFoodServices.Admin
{
    public interface IAdminService
    {
        Task EvaluateUserAsync(EvaluateUserDto evaluateUserDto);

        Task<UserDto> GetUserAsync(string email);

        IEnumerable<UserDto> GetAllUsers(Guid userId);
    }
}