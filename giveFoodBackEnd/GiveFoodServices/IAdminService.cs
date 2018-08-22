using System.Threading.Tasks;
using GiveFoodServices.Admin.Models;

namespace GiveFoodServices.Admin
{
    public interface IAdminService
    {
        Task EvaluateUser(EvaluateUserDto evaluateUserDto);
    }
}