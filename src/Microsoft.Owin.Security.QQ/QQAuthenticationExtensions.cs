using Owin;
using System;

namespace Microsoft.Owin.Security.QQ
{
    /// <summary>
    ///
    /// </summary>
    public static class QQAuthenticationExtensions
    {
        /// <summary>
        /// QQ授权登录
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IAppBuilder UseQQAuthentication(this IAppBuilder app,
            QQAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            app.Use(typeof(QQAuthenticationMiddleware), app, options);

            return app;
        }

        /// <summary>
        /// QQ授权登录
        /// </summary>
        /// <param name="app"></param>
        /// <param name="appId">应用ID</param>
        /// <param name="appSecret">应用密钥</param>
        /// <returns></returns>
        public static IAppBuilder UseQQAuthentication(this IAppBuilder app, string appId, string appSecret)
        {
            return app.UseQQAuthentication(new QQAuthenticationOptions
            {
                AppId = appId,
                AppSecret = appSecret
            });
        }
    }
}