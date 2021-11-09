/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/ArcherTrister/LeXun.Security.OAuth
 * for more information concerning the license and the contributors participating to this project.
 */

namespace Microsoft.AspNetCore.Security.Authentication.WeChatOpen
{
    /// <summary>
    /// Contains constants specific to the <see cref="WeChatOpenAuthenticationHandler"/>.
    /// </summary>
    public static class WeChatOpenAuthenticationConstants
    {
        public static class Claims
        {
            /// <summary>
            /// Unionid
            /// </summary>
            public const string Unionid = "urn:wechatopen:unionid";
            /// <summary>
            /// OpenId
            /// </summary>
            public const string OpenId = "urn:wechatopen:openid";

            /// <summary>
            /// 昵称
            /// </summary>
            public const string NickName = "urn:wechatopen:nickname";

            /// <summary>
            /// 语言
            /// </summary>
            public const string Language = "urn:wechatopen:language";

            /// <summary>
            /// 城市
            /// </summary>
            public const string City = "urn:wechatopen:city";

            /// <summary>
            /// 省份
            /// </summary>
            public const string Province = "urn:wechatopen:province";

            /// <summary>
            /// 国家
            /// </summary>
            public const string Country = "urn:wechatopen:country";

            /// <summary>
            /// 头像
            /// </summary>
            public const string HeadImgUrl = "urn:wechatopen:headimgurl";

            /// <summary>
            /// 特权
            /// </summary>
            public const string Privilege = "urn:wechatopen:privilege";
        }
    }
}