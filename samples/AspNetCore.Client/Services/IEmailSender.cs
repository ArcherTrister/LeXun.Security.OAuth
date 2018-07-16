using System.Threading.Tasks;

namespace AspNetCore.Client.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
