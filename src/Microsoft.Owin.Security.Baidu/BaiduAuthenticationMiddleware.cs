using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using System;
using System.Globalization;
using System.Net.Http;

namespace Microsoft.Owin.Security.Baidu
{
    /// <summary>
    ///
    /// </summary>
    public class BaiduAuthenticationMiddleware : AuthenticationMiddleware<BaiduAuthenticationOptions>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="next"></param>
        /// <param name="app"></param>
        /// <param name="options"></param>
        public BaiduAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app,
            BaiduAuthenticationOptions options)
            : base(next, options)
        {
            if (string.IsNullOrWhiteSpace(Options.AppId))
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                    Resources.Exception_OptionMustBeProvided, nameof(Options.AppId)));
            if (string.IsNullOrWhiteSpace(Options.AppSecret))
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                    Resources.Exception_OptionMustBeProvided, nameof(Options.AppSecret)));

            _logger = app.CreateLogger<BaiduAuthenticationMiddleware>();

            if (Options.Provider == null)
                Options.Provider = new BaiduAuthenticationProvider();

            if (Options.StateDataFormat == null)
            {
                var dataProtector = app.CreateDataProtector(
                    typeof(BaiduAuthenticationMiddleware).FullName,
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
        }

        /// <summary>
        ///     Provides the <see cref="T:Microsoft.Owin.Security.Infrastructure.AuthenticationHandler" /> object for processing
        ///     authentication-related requests.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:Microsoft.Owin.Security.Infrastructure.AuthenticationHandler" /> configured with the
        ///     <see cref="T:Microsoft.Owin.Security.Baidu.BaiduAuthenticationOptions" /> supplied to the constructor.
        /// </returns>
        protected override AuthenticationHandler<BaiduAuthenticationOptions> CreateHandler()
        {
            return new BaiduAuthenticationHandler(_httpClient, _logger);
        }

        private static HttpMessageHandler ResolveHttpMessageHandler(BaiduAuthenticationOptions options)
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