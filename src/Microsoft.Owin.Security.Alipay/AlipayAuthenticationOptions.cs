using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Microsoft.Owin.Security.Alipay
{
    /// <summary>
    ///
    /// </summary>
    public class AlipayAuthenticationOptions : AuthenticationOptions
    {
        /// <summary>
        ///     Initializes a new <see cref="AlipayAuthenticationOptions" />
        /// </summary>
        public AlipayAuthenticationOptions()
            : base(AlipayAuthenticationDefaults.AuthenticationScheme)
        {
            Caption = AlipayAuthenticationDefaults.DisplayName;
            CallbackPath = new PathString(AlipayAuthenticationDefaults.CallbackPath);
            AuthenticationMode = AuthenticationMode.Passive;
            Scope = new List<string>
            {
                "basic"
            };
            BackchannelTimeout = TimeSpan.FromSeconds(60);
            AuthorizationEndPoint = AlipayAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndPoint = AlipayAuthenticationDefaults.TokenEndpoint;
            UserInfoEndPoint = AlipayAuthenticationDefaults.UserInformationEndpoint;
        }

        /// <summary>
        /// Endpoint which is used to redirect users to request Alipay access
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

        /// <summary>
        ///     Gets or sets the a pinned certificate validator to use to validate the endpoints used
        ///     in back channel communications belong to Alipay
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
        ///     The HttpMessageHandler used to communicate with Alipay.
        ///     This cannot be set at the same time as BackchannelCertificateValidator unless the value
        ///     can be downcast to a WebRequestHandler.
        /// </summary>
        public HttpMessageHandler BackchannelHttpHandler { get; set; }

        /// <summary>
        ///     Gets or sets timeout value in milliseconds for back channel communications with Alipay.
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
        /// Gets or sets the Alipay supplied API Key
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets the Alipay supplied Secret Key
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// A list of permissions to request.
        /// </summary>
        public IList<string> Scope { get; private set; }

        /// <summary>
        ///     Gets or sets the <see cref="IAlipayAuthenticationProvider" /> used in the authentication events
        /// </summary>
        public IAlipayAuthenticationProvider Provider { get; set; }

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