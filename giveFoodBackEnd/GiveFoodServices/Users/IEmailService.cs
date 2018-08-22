using System.Threading.Tasks;
using GiveFoodServices.Users.Models;

namespace GiveFoodServices.Users
{
    public interface IEmailService
    {
        AuthMessageSenderOptions Options { get; }

        Task Execute(string apiKey, string subject, string message, string email);
        Task SendEmailAsync(string email, string subject, string message);
    }
}