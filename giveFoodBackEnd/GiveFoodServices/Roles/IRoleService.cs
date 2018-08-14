using System.Threading.Tasks;

namespace GiveFoodServices.Roles
{
    public interface IRoleService
    {
        Task AssignRoleToUser(string userId, string roleName);
        Task CreateRolesandAdminUser();
    }
}
