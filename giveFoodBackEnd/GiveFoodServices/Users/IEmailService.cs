using System.Threading.Tasks;
using GiveFoodServices.Users.Models;
using SendGrid;

namespace GiveFoodServices.Users
{
    public interface IEmailService
    {
        AuthMessageSenderOptions Options { get; }

        Task<Response> ExecuteAsync(string apiKey, string subject, string message, string email);

        Task<Response> SendEmailAsync(string email, string subject, string message);
    }
}