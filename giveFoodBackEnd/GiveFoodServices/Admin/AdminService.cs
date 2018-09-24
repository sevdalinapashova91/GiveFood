using GiveFoodServices.Admin.Models;
using GiveFoodServices.Documents;
using GiveFoodServices.Roles;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using GiveFoodDataModels;
using System.Linq;
using System;

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
            if (evaluateUserDto.IsApproved && await roleService.IsRoleExist(evaluateUserDto.Type.ToString()))
            {
                await this.roleService.AssignRoleToUser(evaluateUserDto.Email, evaluateUserDto.Type.ToString());
            }
            if (!evaluateUserDto.IsApproved)
            {
                var user = await this.userManager.FindByEmailAsync(evaluateUserDto.Email);
                user.Status = UserStatus.NotApproved;
                await this.userManager.UpdateAsync(user);
            }
        }

        public IEnumerable<UserDto> GetAllUsers(Guid userId)
        {
            return this.userManager.Users
                .Where(x=> x.Id != userId)
                .Select(
                user =>
                    new UserDto
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Type = user.Type,
                        Status = user.Status,
                        Description = user.Description
                    }
                )
                .ToList();
        }

        public async Task<UserDto> GetUserAsync(string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);
            var document = await documentService.GetDocumentByUser(user.Id);
            return new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Type = user.Type,
                Description = user.Description,
                Status = user.Status,
                Document = new DocumentDto
                {
                    Name = document?.Name?? string.Empty,
                    Id = document?.Id ?? 0,
                }
            };
        }
    }
}
