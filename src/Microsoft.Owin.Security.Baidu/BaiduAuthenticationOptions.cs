using System;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;

namespace Microsoft.Owin.Security.Baidu
{
    /// <summary>
    /// 
    /// </summary>
    public class BaiduAuthenticationOptions : AuthenticationOptions
    {
        /// <summary>
        ///     Initializes a new <see cref="BaiduAuthenticationOptions" />
        /// </summary>
        public BaiduAuthenticationOptions()
            : base(Constants.AuthenticationScheme)
        {
            Caption = Constants.DisplayName;
            CallbackPath = new PathString(Constants.CallbackPath);
            AuthenticationMode = AuthenticationMode.Passive;
            Scope = new List<string>
            {
                "basic"
            };
            BackchannelTimeout = TimeSpan.FromSeconds(60);
            AuthorizationEndPoint = Constants.AuthorizationEndpoint;
            TokenEndPoint = Constants.TokenEndpoint;
            UserInfoEndPoint = Constants.UserInformationEndpoint;
        }
        /// <summary>
        /// Endpoint which is used to redirect users to request Baidu access
        /// </summary>
        /// <remarks>
        /// Defaults to http://openapi.baidu.com/oauth/2.0/authorize
        /// </remarks>
        public string AuthorizationEndPoint { get; set; }

        /// <summary>
        /// Endpoint which is used to exchange code for access token
        /// </summary>
        /// <remarks>
        /// Defaults to https://openapi.baidu.com/oauth/2.0/token
        /// </remarks>
        public string TokenEndPoint { get; set; }

        /// <summary>
        /// Endpoint which is used to obtain user information after authentication
        /// </summary>
        /// <remarks>
        /// Defaults to https://openapi.baidu.com/rest/2.0/passport/users/getInfo
        /// </remarks>
        public string UserInfoEndPoint { get; set; }
        /// <summary>
        ///     Gets or sets the a pinned certificate validator to use to validate the endpoints used
        ///     in back channel communications belong to Baidu
        /// </summary>
        /// <value>
        ///     The pinned certificate validator.
        /// </value>
        /// <remarks>
        ///     If this property is null then the default certificate checks are performed,
        ///     validating the subject name and if the signing chain is a trusted party.
        /// </remarks>
        public ICertificateValidator BackchannelCertificateValidator { get; set; }

        /// <summary>
        ///     The HttpMessageHandler used to communicate with Baidu.
        ///     This cannot be set at the same time as BackchannelCertificateValidator unless the value
        ///     can be downcast to a WebRequestHandler.
        /// </summary>
        public HttpMessageHandler BackchannelHttpHandler { get; set; }

        /// <summary>
        ///     Gets or sets timeout value in milliseconds for back channel communications with Baidu.
        /// </summary>
        /// <value>
        ///     The back channel timeout in milliseconds.
        /// </value>
        public TimeSpan BackchannelTimeout { get; set; }

        /// <summary>
        ///     The request path within the application's base path where the user-agent will be returned.
        ///     The middleware will process this request when it arrives.
        ///     Default value is "/signin-dropbox".
        /// </summary>
        public PathString CallbackPath { get; set; }

        /// <summary>
        ///     Get or sets the text that the user can display on a sign in user interface.
        /// </summary>
        public string Caption
        {
            get { return Description.Caption; }
            set { Description.Caption = value; }
        }

        /// <summary>
        /// Gets or sets the Baidu supplied API Key
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets the Baidu supplied Secret Key
        /// </summary>
        public string AppSecret { get; set; }
        
        /// <summary>
        /// A list of permissions to request.
        /// </summary>
        public IList<string> Scope { get; private set; }

        /// <summary>
        ///     Gets or sets the <see cref="IBaiduAuthenticationProvider" /> used in the authentication events
        /// </summary>
        public IBaiduAuthenticationProvider Provider { get; set; }

        /// <summary>
        ///     Gets or sets the name of another authentication middleware which will be responsible for actually issuing a user
        ///     <see cref="System.Security.Claims.ClaimsIdentity" />.
        /// </summary>
        public string SignInAsAuthenticationType { get; set; }

        /// <summary>
        ///     Gets or sets the type used to secure data handled by the middleware.
        /// </summary>
        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }


    }
}