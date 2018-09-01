using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Threading.Tasks;

namespace GiveFoodWebApi.Authorization
{
    public interface IAuthorizeService
    {
        Task<HttpResponseMessage> Authorize();
    }
}