using System.Threading.Tasks;

namespace AspNetCore.Client.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string phone, string message);
    }
}
