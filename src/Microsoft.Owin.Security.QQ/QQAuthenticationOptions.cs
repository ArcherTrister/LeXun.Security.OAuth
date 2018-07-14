using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.QQ
{
    public class QQAuthenticationOptions : AuthenticationOptions
    {
        /// <summary>
        ///     Initializes a new <see cref="QQAuthenticationOptions" />
        /// </summary>
        public QQAuthenticationOptions()
            : base(QQAuthenticationDefaults.AuthenticationScheme)
        {
            Caption = QQAuthenticationDefaults.DisplayName;
            CallbackPath = new PathString(QQAuthenticationDefaults.CallbackPath);
            AuthenticationMode = AuthenticationMode.Passive;
            Scope = new List<string>
            {
                "get_user_info"
            };
            BackchannelTimeout = TimeSpan.FromSeconds(60);
            AuthorizationEndPoint = QQAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndPoint = QQAuthenticationDefaults.TokenEndpoint;
            UserInfoEndPoint = QQAuthenticationDefaults.UserInformationEndpoint;
        }

        /// <summary>
        /// Endpoint which is used to redirect users to request Tencent access
        /// </summary>
        /// <remarks>
        /// Defaults to https://graph.qq.com/oauth2.0/authorize 
        /// </remarks>
        public string AuthorizationEndPoint { get; set; }

        /// <summary>
        /// Endpoint which is used to exchange code for access token
        /// </summary>
        /// <remarks>
        /// Defaults to https://graph.qq.com/oauth2.0/token 
        /// </remarks>
        public string TokenEndPoint { get; set; }

        /// <summary>
        /// Endpoint which is used to obtain user information after authentication
        /// </summary>
        /// <remarks>
        /// Defaults to https://graph.qq.com/oauth2.0/me
        /// </remarks>
        public string UserInfoEndPoint { get; set; }

        /// <summary>
        ///     Gets or sets the a pinned certificate validator to use to validate the endpoints used
        ///     in back channel communications belong to Tencent
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
        ///     The HttpMessageHandler used to communicate with Tencent.
        ///     This cannot be set at the same time as BackchannelCertificateValidator unless the value
        ///     can be downcast to a WebRequestHandler.
        /// </summary>
        public HttpMessageHandler BackchannelHttpHandler { get; set; }

        /// <summary>
        ///     Gets or sets timeout value in milliseconds for back channel communications with Tencent.
        /// </summary>
        /// <value>
        ///     The back channel timeout in milliseconds.
        /// </value>
        public TimeSpan BackchannelTimeout { get; set; }

        /// <summary>
        ///     The request path within the application's base path where the user-agent will be returned.
        ///     The middleware will process this request when it arrives.
        ///     Default value is "/sign-in".
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
        ///     Gets or sets the Tencent supplied Application Id
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        ///     Gets or sets the Tencent supplied Application Secret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        ///  Gets or sets the schema
        /// </summary>
        public string Schema { get; set; }


        /// <summary>
        ///     Gets or sets the <see cref="IQQAuthenticationProvider" /> used in the authentication events
        /// </summary>
        public IQQAuthenticationProvider Provider { get; set; }

        /// <summary>
        /// A list of permissions to request.
        /// </summary>
        public IList<string> Scope { get; private set; }

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