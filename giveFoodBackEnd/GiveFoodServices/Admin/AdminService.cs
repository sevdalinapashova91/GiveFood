using GiveFoodServices.Admin.Models;
using GiveFoodServices.Documents;
using GiveFoodServices.Roles;
using System.Threading.Tasks;

namespace GiveFoodServices.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IDocumentService documentService;
        private readonly IRoleService roleService;

        public AdminService(IDocumentService documentService, IRoleService roleService)
        {
            this.documentService = documentService;
            this.roleService = roleService;
        }

        public async Task EvaluateUser(EvaluateUserDto evaluateUserDto)
        {
            await documentService.Approve(evaluateUserDto.DocumentId, evaluateUserDto.IsApproved);
            if (evaluateUserDto.IsApproved && await roleService.IsRoleExist(evaluateUserDto.Type.ToString()))
            {
                await this.roleService.AssignRoleToUser(evaluateUserDto.Email, evaluateUserDto.Type.ToString());
            }
        }
    }
}
