using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.WeiChat.Provider
{
    public class WeiChatAuthenticatedContext : BaseContext
    {
        public WeiChatAuthenticatedContext(IOwinContext context, JObject user, string accessToken)//, int expiresIn
            : base(context)
        {

            User = user;
            AccessToken = accessToken;

            UserId = TryGetValue(user, "openid");
            UserName = TryGetValue(user, "nickname");
        }

        public JObject User { get; private set; }
        public string AccessToken { get; private set; }

        public string UserId { get; private set; }
        public string UserName { get; private set; }

        public ClaimsIdentity Identity { get; set; }
        public AuthenticationProperties Properties { get; set; }

        private static string TryGetValue(JObject user, string propertyName)
        {
            return user.TryGetValue(propertyName, out JToken value) ? value.ToString() : null;
        }
    }
}
