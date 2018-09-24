using System.Threading.Tasks;
using GiveFoodServices.Users.Models;

namespace GiveFoodServices.Users
{
    public interface IProfileService
    {
        Task<ProfileDto> GetAsync(string id);
        Task UpdateAsync(string name, string id, string description);
    }
}