namespace Microsoft.Owin.Security.Baidu
{
    internal static class Constants
    {

        //public const string StateCookie = "_BaiduState";

        /// <summary>
        /// Default value.
        /// </summary>
        public const string AuthenticationScheme = "Baidu";

        /// <summary>
        /// Default value.
        /// </summary>
        public const string DisplayName = "Baidu";

        /// <summary>
        /// Default value for <see cref="BaiduAuthenticationOptions.CallbackPath"/>.
        /// </summary>
        public const string CallbackPath = "/signin-baidu";

        ///// <summary>
        ///// Default value for <see cref="BaiduAuthenticationOptions.ClaimsIssuer"/>.
        ///// </summary>
        //public const string Issuer = "Baidu";

        /// <summary>
        /// Default value for <see cref="BaiduAuthenticationOptions.AuthorizationEndPoint"/>.
        /// </summary>
        public const string AuthorizationEndpoint = "http://openapi.baidu.com/oauth/2.0/authorize";

        /// <summary>
        /// Default value for <see cref="BaiduAuthenticationOptions.TokenEndPoint"/>.
        /// </summary>
        public const string TokenEndpoint = "https://openapi.baidu.com/oauth/2.0/token";

        /// <summary>
        /// Default value for <see cref="BaiduAuthenticationOptions.UserInfoEndPoint"/>.
        /// </summary>
        public const string UserInformationEndpoint = "https://openapi.baidu.com/rest/2.0/passport/users/getInfo";
    }
}