using Owin;
using System;

namespace Microsoft.Owin.Security.Alipay
{
    /// <summary>
    ///
    /// </summary>
    public static class AlipayAuthenticationExtensions
    {
        /// <summary>
        /// Alipay授权登录
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IAppBuilder UseAlipayAuthentication(this IAppBuilder app,
            AlipayAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            app.Use(typeof(AlipayAuthenticationMiddleware), app, options);

            return app;
        }

        /// <summary>
        /// Alipay授权登录
        /// </summary>
        /// <param name="app"></param>
        /// <param name="appId">应用ID</param>
        /// <param name="appSecret">应用密钥</param>
        /// <returns></returns>
        public static IAppBuilder UseAlipayAuthentication(this IAppBuilder app, string appId, string appSecret, string alipayPublicKey)
        {
            return app.UseAlipayAuthentication(new AlipayAuthenticationOptions
            {
                AppId = appId,
                AppSecret = appSecret,
                AlipayPublicKey = alipayPublicKey
            });
        }
    }
}