/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/ArcherTrister/LeXun.Security.OAuth
 * for more information concerning the license and the contributors participating to this project.
 */

namespace Microsoft.AspNetCore.Security.Authentication.WeChatPublic
{
    /// <summary>
    /// Contains constants specific to the <see cref="WeChatPublicAuthenticationHandler"/>.
    /// </summary>
    public static class WeChatPublicAuthenticationConstants
    {
        public static class Claims
        {
            /// <summary>
            /// Unionid
            /// </summary>
            public const string Unionid = "urn:wechatpublic:unionid";
            /// <summary>
            /// OpenId
            /// </summary>
            public const string OpenId = "urn:wechatpublic:openid";

            /// <summary>
            /// 昵称
            /// </summary>
            public const string NickName = "urn:wechatpublic:nickname";

            /// <summary>
            /// 语言
            /// </summary>
            public const string Language = "urn:wechatpublic:language";

            /// <summary>
            /// 城市
            /// </summary>
            public const string City = "urn:wechatpublic:city";

            /// <summary>
            /// 省份
            /// </summary>
            public const string Province = "urn:wechatpublic:province";

            /// <summary>
            /// 国家
            /// </summary>
            public const string Country = "urn:wechatpublic:country";

            /// <summary>
            /// 头像
            /// </summary>
            public const string HeadImgUrl = "urn:wechatpublic:headimgurl";

            /// <summary>
            /// 特权
            /// </summary>
            public const string Privilege = "urn:wechatpublic:privilege";
        }
    }
}