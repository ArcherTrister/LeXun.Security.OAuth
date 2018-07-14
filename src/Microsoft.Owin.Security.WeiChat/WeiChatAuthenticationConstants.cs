using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.WeiChat
{
    public static class WeiChatAuthenticationConstants
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
