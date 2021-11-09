using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.Owin.Security.Alipay.AlipayAuthenticationConstants;

namespace Microsoft.Owin.Security.Alipay
{
    /// <summary>
    ///
    /// </summary>
    public class AlipayAuthenticationHandler : AuthenticationHandler<AlipayAuthenticationOptions>
    {
        private const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly DefaultAopClient _alipayClient;

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="logger"></param>
        public AlipayAuthenticationHandler(HttpClient httpClient, ILogger logger, DefaultAopClient alipayClient)
        {
            _httpClient = httpClient;
            _logger = logger;
            _alipayClient = alipayClient;
            //if (_alipayClient == null)
            //{
            //    _alipayClient = new DefaultAopClient(Options.GatewayUrl,
            //        Options.AppId,
            //        Options.AppSecret,
            //        Options.Format,
            //        Options.Version,
            //        Options.SignType,
            //        Options.AlipayPublicKey,
            //        Options.CharSet,
            //        Options.IsKeyFromFile);
            //}
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            AuthenticationProperties properties = null;

            try
            {
                string code = null;
                string state = null;

                IReadableStringCollection query = Request.Query;
                IList<string> values = query.GetValues("auth_code");
                if (values != null && values.Count == 1)
                {
                    code = values[0];
                }
                values = query.GetValues("state");
                if (values != null && values.Count == 1)
                {
                    state = values[0];
                }

                properties = Options.StateDataFormat.Unprotect(state);
                if (properties == null)
                {
                    return null;
                }

                // OAuth2 10.12 CSRF
                if (!ValidateCorrelationId(properties, _logger))
                {
                    return new AuthenticationTicket(null, properties);
                }

                // Check for error
                if (Request.Query.Get("error") != null)
                    return new AuthenticationTicket(null, properties);

                var alipayRequest = new AlipaySystemOauthTokenRequest
                {
                    Code = code,
                    GrantType = "authorization_code"
                    //GetApiName()
                };

                AlipaySystemOauthTokenResponse alipayResponse = _alipayClient.Execute(alipayRequest);
                if (alipayResponse.IsError)
                {
                    _logger.WriteWarning("An error occurred while retrieving an access token.");
                    return new AuthenticationTicket(null, properties);
                }
                else
                {
                    // Request the token
                    //var response = JObject.Parse(alipayResponse.Body);
                    //dynamic tokens = new
                    //{
                    //    Response = response,
                    //    AccessToken = response["alipay_system_oauth_token_response"].Value<string>("access_token"),
                    //    TokenType = response["alipay_system_oauth_token_response"].Value<string>("token_type"),
                    //    RefreshToken = response["alipay_system_oauth_token_response"].Value<string>("refresh_token"),
                    //    ExpiresIn = response["alipay_system_oauth_token_response"].Value<string>("expires_in")
                    //};
                    //var Response = response;
                    //var AccessToken = alipayResponse.AccessToken;
                    //var TokenType = response.Value<string>("token_type");
                    //var RefreshToken = response.alipay_system_oauth_token_response.expires_in;
                    //var ExpiresIn = response.Value<string>("expires_in");


                    // Get the Alipay user
                    var requestUser = new AlipayUserInfoShareRequest();
                    AlipayUserInfoShareResponse userinfoShareResponse = _alipayClient.Execute(requestUser, alipayResponse.AccessToken);
                    if (userinfoShareResponse.IsError)
                    {
                        _logger.WriteWarning("An error occurred while retrieving user information.");
                        throw new HttpRequestException("An error occurred while retrieving user information.");
                    }
                    else
                    {
                        //var user = JObject.FromObject(userinfoShareResponse);
                        var context = new AlipayAuthenticatedContext(Context, userinfoShareResponse, alipayResponse.AccessToken, Convert.ToInt32(alipayResponse.ExpiresIn))
                        {
                            Identity = new ClaimsIdentity(
                                Options.AuthenticationType,
                                ClaimsIdentity.DefaultNameClaimType,
                                ClaimsIdentity.DefaultRoleClaimType)
                        };
                        if (!string.IsNullOrEmpty(context.UserId))
                        {
                            context.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.UserId, XmlSchemaString, Options.AuthenticationType));
                        }
                        if (!string.IsNullOrEmpty(context.UserName))
                        {
                            context.Identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, context.UserName, XmlSchemaString, Options.AuthenticationType));
                        }
                        context.Properties = properties;

                        await Options.Provider.Authenticated(context);

                        return new AuthenticationTicket(context.Identity, context.Properties);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex.Message);
            }
            return new AuthenticationTicket(null, properties);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401)
            {
                return Task.FromResult<object>(null);
            }

            var challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);

            if (challenge == null) return Task.FromResult<object>(null);
            var baseUri =
                Request.Scheme +
                Uri.SchemeDelimiter +
                Request.Host +
                Request.PathBase;

            var currentUri =
                baseUri +
                Request.Path +
                Request.QueryString;

            var redirectUri =
                baseUri +
                Options.CallbackPath;

            var properties = challenge.Properties;
            if (string.IsNullOrEmpty(properties.RedirectUri))
            {
                properties.RedirectUri = currentUri;
            }

            // OAuth2 10.12 CSRF
            GenerateCorrelationId(properties);

            // comma separated
            var scope = string.Join(",", Options.Scope);

            var state = Options.StateDataFormat.Protect(properties);

            var authorizationEndpoint = Options.AuthorizationEndPoint +
                "?goto=" + Uri.EscapeDataString(Options.AuthorizationGotoPoint +
                "?app_id=" + Options.AppId +
                "&scope=" + scope +
                "&redirect_uri=" + redirectUri +
                "&state=" + state);


            //var cookieOptions = new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = Request.IsSecure
            //};

            //Response.StatusCode = 302;
            //Response.Cookies.Append(Constants.StateCookie, Options.StateDataFormat.Protect(properties), cookieOptions);
            //Response.Headers.Set("Location", authorizationEndpoint);
            Response.Redirect(authorizationEndpoint);
            return Task.FromResult<object>(null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> InvokeAsync()
        {
            return await InvokeReplyPathAsync();
        }

        private async Task<bool> InvokeReplyPathAsync()
        {
            if (!Options.CallbackPath.HasValue || Options.CallbackPath != Request.Path) return false;
            // TODO: error responses

            var ticket = await AuthenticateAsync();
            if (ticket == null)
            {
                _logger.WriteWarning("Invalid return state, unable to redirect.");
                Response.StatusCode = 500;
                return true;
            }

            var context = new AlipayReturnEndpointContext(Context, ticket)
            {
                SignInAsAuthenticationType = Options.SignInAsAuthenticationType,
                RedirectUri = ticket.Properties.RedirectUri
            };

            await Options.Provider.ReturnEndpoint(context);

            if (context.SignInAsAuthenticationType != null &&
                context.Identity != null)
            {
                var grantIdentity = context.Identity;
                if (!string.Equals(grantIdentity.AuthenticationType, context.SignInAsAuthenticationType, StringComparison.Ordinal))
                {
                    grantIdentity = new ClaimsIdentity(grantIdentity.Claims, context.SignInAsAuthenticationType, grantIdentity.NameClaimType, grantIdentity.RoleClaimType);
                }
                Context.Authentication.SignIn(context.Properties, grantIdentity);
            }

            if (context.IsRequestCompleted || context.RedirectUri == null) return context.IsRequestCompleted;
            var redirectUri = context.RedirectUri;
            if (context.Identity == null)
            {
                // add a redirect hint that sign-in failed in some way
                redirectUri = WebUtilities.AddQueryString(redirectUri, "error", "access_denied");
            }
            Response.Redirect(redirectUri);
            context.RequestCompleted();

            return context.IsRequestCompleted;
        }
    }
}