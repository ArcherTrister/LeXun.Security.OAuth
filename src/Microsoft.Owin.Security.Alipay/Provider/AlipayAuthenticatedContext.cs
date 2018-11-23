// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using Aop.Api.Response;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Claims;

namespace Microsoft.Owin.Security.Alipay
{
    /// <summary>
    /// Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.
    /// </summary>
    public class AlipayAuthenticatedContext : BaseContext
    {
        /// <summary>
        /// Initializes a <see cref="AlipayAuthenticatedContext"/>
        /// </summary>
        /// <param name="context">The OWIN environment</param>
        /// <param name="user">The JSON-serialized user</param>
        /// <param name="accessToken">Alipay Access token</param>
        public AlipayAuthenticatedContext(IOwinContext context, AlipayUserInfoShareResponse user, string accessToken, int expiresIn)
            : base(context)
        {
            AccessToken = accessToken;
            ExpiresIn = TimeSpan.FromSeconds(expiresIn);
            User = user;
            UserId = user.UserId;
            UserName = user.UserName;
            NickName = user.NickName;
            //RealName = TryGetValue(user, "realname");
            //ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_id");
            //ClaimActions.MapJsonKey(ClaimTypes.Name, "nick_name");
            ////【注意】只有is_certified为T的时候才有意义，否则不保证准确性. 性别（F：女性；M：男性）。
            //ClaimActions.MapJsonKey(ClaimTypes.Gender, "gender", ClaimValueTypes.Integer);
            //ClaimActions.MapJsonKey(Claims.Avatar, "avatar");
            //ClaimActions.MapJsonKey(Claims.Province, "province");
            //ClaimActions.MapJsonKey(Claims.City, "city");
            //ClaimActions.MapJsonKey(Claims.IsStudentCertified, "is_student_certified");
            //ClaimActions.MapJsonKey(Claims.UserType, "user_type");
            //ClaimActions.MapJsonKey(Claims.UserStatus, "user_status");
            //ClaimActions.MapJsonKey(Claims.IsCertified, "is_certified");
        }

        /// <summary>
        /// Gets the Tencent access token expiration time
        /// </summary>
        public TimeSpan? ExpiresIn { get; set; }

        /// <summary>
        /// Gets the JSON-serialized user
        /// </summary>
        /// <remarks>
        /// Contains the Alipay user obtained from the endpoint https://api.dropbox.com/1/account/info
        /// </remarks>
        public AlipayUserInfoShareResponse User { get; private set; }

        /// <summary>
        /// Gets the Alipay OAuth access token
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets the Alipay user ID
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// The name of the user
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// The nickname of the user
        /// </summary>
        public string NickName { get; private set; }

        ///// <summary>
        ///// The real name of the user
        ///// </summary>
        //public string RealName { get; private set; }

        /// <summary>
        /// Gets the <see cref="ClaimsIdentity"/> representing the user
        /// </summary>
        public ClaimsIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets a property bag for common authentication properties
        /// </summary>
        public AuthenticationProperties Properties { get; set; }

        private static string TryGetValue(JObject user, string propertyName)
        {
            JToken value;
            return user.TryGetValue(propertyName, out value) ? value.ToString() : null;
        }
    }
}