using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Security.Authentication.Sina
{
    /// <summary>
    /// Contains constants specific to the <see cref="WeiboAuthenticationHandler"/>.
    /// </summary>
    public static class WeiboAuthenticationConstants
    {
        public static class Claims
        {
            public const string AvatarHd = "urn:weibo:avatar_hd";
            public const string AvatarLarge = "urn:weibo:avatar_large";
            public const string CoverImagePhone = "urn:weibo:cover_image_phone";
            public const string Location = "urn:weibo:location";
            public const string ProfileImageUrl = "urn:weibo:profile_image_url";
            public const string ScreenName = "urn:weibo:screen_name";
        }
    }
}
