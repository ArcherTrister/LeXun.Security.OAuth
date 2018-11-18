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
using static Microsoft.AspNetCore.Security.Authentication.WeChatPublic.WeChatPublicAuthenticationConstants;

namespace Microsoft.AspNetCore.Security.Authentication.WeChatPublic
{
    /// <summary>
    /// Defines a set of options used by <see cref="WeChatPublicAuthenticationHandler"/>.
    /// </summary>
    public class WeChatPublicAuthenticationOptions : OAuthOptions
    {
        public WeChatPublicAuthenticationOptions()
        {
            ClaimsIssuer = WeChatPublicAuthenticationDefaults.Issuer;
            CallbackPath = new PathString(WeChatPublicAuthenticationDefaults.CallbackPath);

            AuthorizationEndpoint = WeChatPublicAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = WeChatPublicAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = WeChatPublicAuthenticationDefaults.UserInformationEndpoint;
            IsRelation = WeChatPublicAuthenticationDefaults.IsRelation;
            //Scope.Add("snsapi_login");
            Scope.Add("snsapi_userinfo");
            //如果在微信开放平台做了关联，则取Unionid，反之取Openid
            if (IsRelation)
            {
                ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "unionid");
            }
            else {
                ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "openid");
            }

            ClaimActions.MapJsonKey(ClaimTypes.Name, "nickname");
            ClaimActions.MapJsonKey(ClaimTypes.Gender, "sex", ClaimValueTypes.Integer);
            ClaimActions.MapJsonKey(Claims.Unionid, "unionid");
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

        /// <summary>
        /// 是否在微信开发平台做了关联
        /// </summary>
        public bool IsRelation { get; set; }
    }
}