using System.Threading.Tasks;

namespace Microsoft.Owin.Security.WeiChat.Provider
{
    public interface IWeiChatAuthenticationProvider
    {
        Task Authenticated(WeiChatAuthenticatedContext context);

        Task ReturnEndpoint(WeiChatReturnEndpointContext context);
    }
}