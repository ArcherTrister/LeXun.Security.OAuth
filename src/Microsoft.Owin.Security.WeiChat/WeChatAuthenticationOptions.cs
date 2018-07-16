using Microsoft.Owin.Security.WeiChat.Provider;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Microsoft.Owin.Security.WeiChat
{
    public class WeiChatAuthenticationOptions : AuthenticationOptions
    {
        public WeiChatAuthenticationOptions()
            : base(WeiChatAuthenticationDefaults.AuthenticationScheme)
        {
            Caption = WeiChatAuthenticationDefaults.DisplayName;
            CallbackPath = new PathString(WeiChatAuthenticationDefaults.CallbackPath);
            AuthenticationMode = AuthenticationMode.Passive;
            Scope = new List<string> { "get_user_info" };
            BackchannelTimeout = TimeSpan.FromSeconds(60);
            AuthorizationEndPoint = WeiChatAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndPoint = WeiChatAuthenticationDefaults.TokenEndpoint;
            UserInfoEndPoint = WeiChatAuthenticationDefaults.UserInformationEndpoint;
        }

        /// <summary>
        /// Endpoint which is used to redirect users to request Tencent access
        /// </summary>
        public string AuthorizationEndPoint { get; set; }

        /// <summary>
        /// Endpoint which is used to exchange code for access token
        /// </summary>
        public string TokenEndPoint { get; set; }

        /// <summary>
        /// Endpoint which is used to obtain user information after authentication
        /// </summary>
        public string UserInfoEndPoint { get; set; }

        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }

        public TimeSpan BackchannelTimeout { get; set; }

        public WebRequestHandler BackchannelHttpHandler { get; set; }

        public IWeiChatAuthenticationProvider Provider { get; set; }

        public ICertificateValidator BackchannelCertificateValidator { get; set; }

        public IList<string> Scope { get; private set; }

        /// <summary>
        ///  Gets or sets the schema
        /// </summary>
        public string Schema { get; set; }

        public PathString CallbackPath { get; set; }

        public string SignInAsAuthenticationType { get; set; }

        public string Caption
        {
            get { return Description.Caption; }
            set { Description.Caption = value; }
        }

        public string AppId { get; set; }

        public string AppSecret { get; set; }
    }
}