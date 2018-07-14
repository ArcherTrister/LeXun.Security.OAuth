using Owin;
using System;

namespace Microsoft.Owin.Security.Baidu
{
    /// <summary>
    /// 
    /// </summary>
    public static class BaiduAuthenticationExtensions
    {
        /// <summary>
        /// Baidu授权登录
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IAppBuilder UseBaiduAuthentication(this IAppBuilder app,
            BaiduAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            app.Use(typeof(BaiduAuthenticationMiddleware), app, options);

            return app;
        }

        /// <summary>
        /// Baidu授权登录
        /// </summary>
        /// <param name="app"></param>
        /// <param name="appId">应用ID</param>
        /// <param name="appSecret">应用密钥</param>
        /// <returns></returns>
        public static IAppBuilder UseBaiduAuthentication(this IAppBuilder app, string appId, string appSecret)
        {
            return app.UseBaiduAuthentication(new BaiduAuthenticationOptions
            {
                AppId = appId,
                AppSecret = appSecret
            });
        }
    }
}