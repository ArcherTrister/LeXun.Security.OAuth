using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Sina
{
    public class SinaAuthenticationEndpoints
    {
        /// <summary>
        /// Endpoint which is used to redirect users to request Sina access
        /// </summary>
        /// <remarks>
        /// Defaults to https://api.weibo.com/oauth2/authorize
        /// </remarks>
        public string AuthorizationEndpoint { get; set; }

        /// <summary>
        /// Endpoint which is used to exchange code for access token
        /// </summary>
        /// <remarks>
        /// Defaults to https://api.weibo.com/oauth2/access_token
        /// </remarks>
        public string TokenEndpoint { get; set; }

        /// <summary>
        /// Endpoint which is used to obtain user information after authentication
        /// </summary>
        /// <remarks>
        /// Defaults to https://api.weibo.com/oauth2/get_token_info
        /// </remarks>
        public string UserInfoEndpoint { get; set; }
    }
}
