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
                "auth_user"
            };
            BackchannelTimeout = TimeSpan.FromSeconds(60);
            AuthorizationEndPoint = AlipayAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndPoint = AlipayAuthenticationDefaults.TokenEndpoint;
            UserInfoEndPoint = AlipayAuthenticationDefaults.UserInformationEndpoint;

            AuthorizationGotoPoint = AlipayAuthenticationDefaults.AuthorizationGotoPoint;

            GatewayUrl = AlipayAuthenticationDefaults.GatewayUrl;
            AlipayPublicKey = AlipayAuthenticationDefaults.AlipayPublicKey;
            SignType = AlipayAuthenticationDefaults.SignType;
            CharSet = AlipayAuthenticationDefaults.CharSet;
            Version = AlipayAuthenticationDefaults.Version;
            Format = AlipayAuthenticationDefaults.Format;
            IsKeyFromFile = AlipayAuthenticationDefaults.IsKeyFromFile;
        }

        /// <summary>
        /// Endpoint which is used to redirect users to request Alipay access
        /// </summary>
        public string AuthorizationEndPoint { get; set; }

        /// <summary>
        /// Endpoint which is used to redirect users to request Alipay access
        /// </summary>
        public string AuthorizationGotoPoint { get; set; }

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
        /// Gets or sets the Alipay supplied AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets the Alipay supplied AppSecret
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

        /// <summary>
        /// 支付宝网关
        /// </summary>
        public string GatewayUrl { get; set; }

        /// <summary>
        /// 支付宝公钥
        /// </summary>
        public string AlipayPublicKey { get; set; }

        /// <summary>
        /// 签名方式
        /// </summary>
        public string SignType { get; set; }

        /// <summary>
        /// 编码格式
        /// </summary>
        public string CharSet { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// 数据格式
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// 是否从文件读取公私钥
        /// </summary>
        public bool IsKeyFromFile { get; set; }
    }
}