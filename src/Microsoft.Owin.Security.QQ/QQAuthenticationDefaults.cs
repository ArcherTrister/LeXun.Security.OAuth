using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Microsoft.Owin.Security.QQ
{
    /// <summary>
    /// Default values for QQ authentication.
    /// </summary>
    public static class QQAuthenticationDefaults
    {
        /// <summary>
        /// Default value.
        /// </summary>
        public const string AuthenticationScheme = "QQ";

        /// <summary>
        /// Default value.
        /// </summary>
        public const string DisplayName = "QQ";

        /// <summary>
        /// Default value.
        /// </summary>
        public const string Issuer = "QQ";

        /// <summary>
        /// Default value for <see cref="QQAuthenticationOptions.CallbackPath"/>.
        /// </summary>
        public const string CallbackPath = "/signin-qq";

        /// <summary>
        /// Default value for <see cref="QQAuthenticationOptions.AuthorizationEndPoint"/>.
        /// </summary>
        public const string AuthorizationEndpoint = "https://graph.qq.com/oauth2.0/authorize";

        /// <summary>
        /// Default value for <see cref="QQAuthenticationOptions.TokenEndPoint"/>.
        /// </summary>
        public const string TokenEndpoint = "https://graph.qq.com/oauth2.0/token";

        ///// <summary>
        ///// Default value for <see cref="QQAuthenticationOptions.UserIdentificationEndpoint"/>.
        ///// </summary>
        //public const string UserIdentificationEndpoint = "https://graph.qq.com/oauth2.0/me";

        /// <summary>
        /// Default value for <see cref="QQAuthenticationOptions.UserInfoEndPoint"/>.
        /// </summary>
        public const string UserInformationEndpoint = "https://graph.qq.com/oauth2.0/me";
        //"https://graph.qq.com/user/get_user_info";
    }
}