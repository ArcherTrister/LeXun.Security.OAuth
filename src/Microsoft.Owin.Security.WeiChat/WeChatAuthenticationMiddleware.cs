using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.WeiChat.Provider;
using Owin;
using System;
using System.Net.Http;

namespace Microsoft.Owin.Security.WeiChat
{
    public class WeiChatAuthenticationMiddleware : AuthenticationMiddleware<WeiChatAuthenticationOptions>
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public WeiChatAuthenticationMiddleware(
            OwinMiddleware next,
            IAppBuilder app,
            WeiChatAuthenticationOptions options)
            : base(next, options)
        {
            _logger = app.CreateLogger<WeiChatAuthenticationOptions>();

            if (Options.Provider == null)
            {
                Options.Provider = new WeiChatAuthenticationProvider();
            }
            if (Options.StateDataFormat == null)
            {
                var dataProtecter = app.CreateDataProtector(
                    typeof(WeiChatAuthenticationMiddleware).FullName,
                    Options.AuthenticationType, "v1");
                Options.StateDataFormat = new PropertiesDataFormat(dataProtecter);
            }

            _httpClient = new HttpClient(ResolveHttpMessageHandler(Options));
            _httpClient.Timeout = Options.BackchannelTimeout;
            _httpClient.MaxResponseContentBufferSize = 1024 * 1024 * 10; // 10 MB
        }

        protected override AuthenticationHandler<WeiChatAuthenticationOptions> CreateHandler()
        {
            return new WeiChatAuthenticationHandler(_httpClient, _logger);
        }

        private static HttpMessageHandler ResolveHttpMessageHandler(WeiChatAuthenticationOptions options)
        {
            HttpMessageHandler handler = options.BackchannelHttpHandler ?? new WebRequestHandler();

            // If they provided a validator, apply it or fail.
            if (options.BackchannelCertificateValidator != null)
            {
                // Set the cert validate callback
                WebRequestHandler webRequestHandler = handler as WebRequestHandler;
                if (webRequestHandler == null)
                {
                    throw new InvalidOperationException("An ICertificateValidator cannot be specified at the same time as an HttpMessageHandler unless it is a WebRequestHandler.");
                }
                webRequestHandler.ServerCertificateValidationCallback = options.BackchannelCertificateValidator.Validate;
            }

            return handler;
        }
    }
}