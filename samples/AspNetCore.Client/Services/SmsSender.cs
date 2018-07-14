using System;
using System.Threading.Tasks;

namespace AspNetCore.Client.Services
{
    public class SmsSender : ISmsSender
    {
        public Task SendSmsAsync(string phone, string message)
        {
            throw new NotImplementedException();
        }
    }
}
