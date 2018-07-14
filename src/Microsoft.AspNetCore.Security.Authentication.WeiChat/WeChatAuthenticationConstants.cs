/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/ArcherTrister/Security.OAuth
 * for more information concerning the license and the contributors participating to this project.
 */

namespace Microsoft.AspNetCore.Security.Authentication.WeChat
{
    /// <summary>
    /// Contains constants specific to the <see cref="WeChatAuthenticationHandler"/>.
    /// </summary>
    public static class WeChatAuthenticationConstants
    {
        public static class Claims
        {
            /// <summary>
            /// OpenId
            /// </summary>
            public const string OpenId = "urn:weichat:openid";

            /// <summary>
            /// 昵称
            /// </summary>
            public const string NickName = "urn:weichat:nickname";

            /// <summary>
            /// 语言
            /// </summary>
            public const string Language = "urn:weichat:language";

            /// <summary>
            /// 城市
            /// </summary>
            public const string City = "urn:weichat:city";

            /// <summary>
            /// 省份
            /// </summary>
            public const string Province = "urn:weichat:province";

            /// <summary>
            /// 国家
            /// </summary>
            public const string Country = "urn:weichat:country";

            /// <summary>
            /// 头像
            /// </summary>
            public const string HeadImgUrl = "urn:weichat:headimgurl";

            /// <summary>
            /// 特权
            /// </summary>
            public const string Privilege = "urn:weichat:privilege";
        }
    }
}