/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/ArcherTrister/LeXun.Security.OAuth
 * for more information concerning the license and the contributors participating to this project.
 */

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using static Microsoft.AspNetCore.Security.Authentication.WeiChat.WeiChatAuthenticationConstants;

namespace Microsoft.AspNetCore.Security.Authentication.WeiChat
{
    /// <summary>
    /// Defines a set of options used by <see cref="WeiChatAuthenticationHandler"/>.
    /// </summary>
    public class WeiChatAuthenticationOptions : OAuthOptions
    {
        public WeiChatAuthenticationOptions()
        {
            ClaimsIssuer = WeiChatAuthenticationDefaults.Issuer;
            CallbackPath = new PathString(WeiChatAuthenticationDefaults.CallbackPath);

            AuthorizationEndpoint = WeiChatAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = WeiChatAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = WeiChatAuthenticationDefaults.UserInformationEndpoint;

            Scope.Add("snsapi_login");
            Scope.Add("snsapi_userinfo");

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "unionid");
            ClaimActions.MapJsonKey(ClaimTypes.Name, "nickname");
            ClaimActions.MapJsonKey(ClaimTypes.Gender, "sex", ClaimValueTypes.Integer);

            ClaimActions.MapJsonKey(Claims.OpenId, "openid");
            ClaimActions.MapJsonKey(Claims.NickName, "nickname");
            ClaimActions.MapJsonKey(Claims.Language, "language");
            ClaimActions.MapJsonKey(Claims.City, "city");
            ClaimActions.MapJsonKey(Claims.Province, "province");
            ClaimActions.MapJsonKey(ClaimTypes.Country, "country");
            ClaimActions.MapJsonKey(Claims.HeadImgUrl, "headimgurl");
            ClaimActions.MapCustomJson(Claims.Privilege, user =>
            {
                var value = user.Value<JArray>("privilege");
                return value == null ? null : string.Join(",", value.ToObject<string[]>());
            });
            //ClaimActions.MapCustomJson(Claims.Privilege, user => string.Join(",", user.SelectToken("privilege")?.Select(s => (string)s).ToArray() ?? new string[0]));
        }
    }
}