using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.WeiChat.Provider
{
    public class WeiChatAuthenticationProvider : IWeiChatAuthenticationProvider
    {
        public WeiChatAuthenticationProvider()
        {
            onAuthenticated = (c) => Task.FromResult<WeiChatAuthenticatedContext>(null);
            onReturnEndpoint = (c) => Task.FromResult<WeiChatReturnEndpointContext>(null);
        }

        public Func<WeiChatAuthenticatedContext, Task> onAuthenticated { get; set; }
        public Func<WeiChatReturnEndpointContext, Task> onReturnEndpoint { get; set; }

        public Task Authenticated(WeiChatAuthenticatedContext context)
        {
            return onAuthenticated(context);
        }

        public Task ReturnEndpoint(WeiChatReturnEndpointContext context)
        {
            return onReturnEndpoint(context);
        }
    }
}