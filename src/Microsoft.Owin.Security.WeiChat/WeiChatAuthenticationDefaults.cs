namespace Microsoft.Owin.Security.WeiChat
{
    /// <summary>
    /// Default values for WeiChat authentication.
    /// </summary>
    public static class WeiChatAuthenticationDefaults
    {
        /// <summary>
        /// Default value.
        /// </summary>
        public const string AuthenticationScheme = "WeiChat";

        /// <summary>
        /// Default value.
        /// </summary>
        public const string DisplayName = "WeiChat";

        /// <summary>
        /// Default value for <see cref="WeiChatAuthenticationOptions.CallbackPath"/>.
        /// </summary>
        public const string CallbackPath = "/signin-weichat";

        /// <summary>
        /// Default value.
        /// </summary>
        public const string Issuer = "WeiChat";

        /// <summary>
        /// Default value for <see cref="WeiChatAuthenticationOptions.AuthorizationEndPoint"/>.
        /// </summary>
        public const string AuthorizationEndpoint = "https://open.weixin.qq.com/connect/qrconnect";

        /// <summary>
        /// Default value for <see cref="WeiChatAuthenticationOptions.TokenEndPoint"/>.
        /// </summary>
        public const string TokenEndpoint = "https://api.weixin.qq.com/sns/oauth2/access_token";

        /// <summary>
        /// Default value for <see cref="WeiChatAuthenticationOptions.UserInfoEndPoint"/>.
        /// </summary>
        public const string UserInformationEndpoint = "https://api.weixin.qq.com/sns/userinfo";
    }
}