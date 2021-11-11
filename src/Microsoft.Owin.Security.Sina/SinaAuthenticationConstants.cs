using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Sina
{
    public static class SinaAuthenticationConstants
    {
        public const string DefaultAuthenticationType = "Sina";

        public const string AuthorizationEndPoint = "https://api.weibo.com/oauth2/authorize";
        public const string TokenEndpoint = "https://api.weibo.com/oauth2/access_token";
    }
}
