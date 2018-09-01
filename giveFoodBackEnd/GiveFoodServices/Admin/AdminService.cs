using GiveFoodServices.Admin.Models;
using GiveFoodServices.Documents;
using GiveFoodServices.Roles;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using GiveFoodDataModels;
using System.Linq;

namespace GiveFoodServices.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IDocumentService documentService;
        private readonly IRoleService roleService;
        private readonly UserManager<User> userManager;

        public AdminService(
            IDocumentService documentService, 
            IRoleService roleService,
            UserManager<User> userManager)
        {
            this.documentService = documentService;
            this.roleService = roleService;
            this.userManager = userManager;
        }

        public async Task EvaluateUserAsync(EvaluateUserDto evaluateUserDto)
        {
            await documentService.ApproveAsync(evaluateUserDto.DocumentId, evaluateUserDto.IsApproved);
            if (evaluateUserDto.IsApproved && await roleService.IsRoleExist(evaluateUserDto.Type.ToString()))
            {
                await this.roleService.AssignRoleToUser(evaluateUserDto.Email, evaluateUserDto.Type.ToString());
            }
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return this.userManager.Users
                .Select(
                user =>
                    new UserDto
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Type = user.Type,
                        IsApproved = user.Document.IsApproved,
                    }
                )
                .ToList();
        }

        public async Task<UserDto> GetUserAsync(string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);
            return new UserDto
            {

                Name = user.Name,
                Email = user.Email,
                Type = user.Type,
                IsApproved = user.Document.IsApproved,
                Document = new DocumentDto
                {
                    Name = user.Document.Name,
                    Id = user.Document.Id
                }
            };
        }
    }
}
