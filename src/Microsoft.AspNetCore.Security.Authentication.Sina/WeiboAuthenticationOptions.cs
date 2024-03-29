﻿using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

using static Microsoft.AspNetCore.Security.Authentication.Sina.WeiboAuthenticationConstants;

namespace Microsoft.AspNetCore.Security.Authentication.Sina
{
    /// <summary>
    /// Defines a set of options used by <see cref="WeiboAuthenticationHandler"/>.
    /// </summary>
    public class WeiboAuthenticationOptions : OAuthOptions
    {
        public WeiboAuthenticationOptions()
        {
            ClaimsIssuer = WeiboAuthenticationDefaults.Issuer;
            CallbackPath = WeiboAuthenticationDefaults.CallbackPath;

            AuthorizationEndpoint = WeiboAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = WeiboAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = WeiboAuthenticationDefaults.UserInformationEndpoint;

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
            ClaimActions.MapJsonKey(ClaimTypes.Gender, "gender");
            ClaimActions.MapJsonKey(Claims.ScreenName, "screen_name");
            ClaimActions.MapJsonKey(Claims.ProfileImageUrl, "profile_image_url");
            ClaimActions.MapJsonKey(Claims.AvatarLarge, "avatar_large");
            ClaimActions.MapJsonKey(Claims.AvatarHd, "avatar_hd");
            ClaimActions.MapJsonKey(Claims.CoverImagePhone, "cover_image_phone");
            ClaimActions.MapJsonKey(Claims.Location, "location");

            Scope.Add("email");
        }

        /// <summary>
        /// Gets or sets the address of the endpoint exposing
        /// the email addresses associated with the logged in user.
        /// </summary>
        public string UserEmailsEndpoint { get; set; } = WeiboAuthenticationDefaults.UserEmailsEndpoint;
    }
}
