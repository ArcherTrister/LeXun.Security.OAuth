using Aop.Api;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using System;
using System.Globalization;
using System.Net.Http;

namespace Microsoft.Owin.Security.Alipay
{
    /// <summary>
    ///
    /// </summary>
    public class AlipayAuthenticationMiddleware : AuthenticationMiddleware<AlipayAuthenticationOptions>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly DefaultAopClient _alipayClient;

        /// <summary>
        ///
        /// </summary>
        /// <param name="next"></param>
        /// <param name="app"></param>
        /// <param name="options"></param>
        public AlipayAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app,
            AlipayAuthenticationOptions options)
            : base(next, options)
        {
            if (string.IsNullOrWhiteSpace(Options.AppId))
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                    Resources.Exception_OptionMustBeProvided, nameof(Options.AppId)));
            if (string.IsNullOrWhiteSpace(Options.AppSecret))
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                    Resources.Exception_OptionMustBeProvided, nameof(Options.AppSecret)));
            if (string.IsNullOrWhiteSpace(Options.AlipayPublicKey))
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                    Resources.Exception_OptionMustBeProvided, nameof(Options.AlipayPublicKey)));

            _logger = app.CreateLogger<AlipayAuthenticationMiddleware>();

            if (Options.Provider == null)
                Options.Provider = new AlipayAuthenticationProvider();

            if (Options.StateDataFormat == null)
            {
                var dataProtector = app.CreateDataProtector(
                    typeof(AlipayAuthenticationMiddleware).FullName,
                    Options.AuthenticationType, "v1");
                Options.StateDataFormat = new PropertiesDataFormat(dataProtector);
            }

            if (string.IsNullOrEmpty(Options.SignInAsAuthenticationType))
                Options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();

            _httpClient = new HttpClient(ResolveHttpMessageHandler(Options))
            {
                Timeout = Options.BackchannelTimeout,
                MaxResponseContentBufferSize = 1024 * 1024 * 10
            };
            _alipayClient = new DefaultAopClient(Options.GatewayUrl,
                Options.AppId,
                Options.AppSecret,
                Options.Format,
                Options.Version,
                Options.SignType,
                Options.AlipayPublicKey,
                Options.CharSet,
                Options.IsKeyFromFile);
        }

        /// <summary>
        ///     Provides the <see cref="T:Microsoft.Owin.Security.Infrastructure.AuthenticationHandler" /> object for processing
        ///     authentication-related requests.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:Microsoft.Owin.Security.Infrastructure.AuthenticationHandler" /> configured with the
        ///     <see cref="T:Microsoft.Owin.Security.Alipay.AlipayAuthenticationOptions" /> supplied to the constructor.
        /// </returns>
        protected override AuthenticationHandler<AlipayAuthenticationOptions> CreateHandler()
        {
            return new AlipayAuthenticationHandler(_httpClient, _logger, _alipayClient);
        }

        private static HttpMessageHandler ResolveHttpMessageHandler(AlipayAuthenticationOptions options)
        {
            var handler = options.BackchannelHttpHandler ?? new WebRequestHandler();

            // If they provided a validator, apply it or fail.
            if (options.BackchannelCertificateValidator == null) return handler;
            // Set the cert validate callback
            if (!(handler is WebRequestHandler webRequestHandler))
            {
                throw new InvalidOperationException(Resources.Exception_ValidatorHandlerMismatch);
            }
            webRequestHandler.ServerCertificateValidationCallback = options.BackchannelCertificateValidator.Validate;

            return handler;
        }
    }
}