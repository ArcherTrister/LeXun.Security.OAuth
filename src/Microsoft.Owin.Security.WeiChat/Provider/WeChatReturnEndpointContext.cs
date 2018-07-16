using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.WeiChat.Provider
{
    public class WeiChatReturnEndpointContext : ReturnEndpointContext
    {
        public WeiChatReturnEndpointContext(
            IOwinContext context,
            AuthenticationTicket ticket)
            : base(context, ticket)
        {
        }
    }
}