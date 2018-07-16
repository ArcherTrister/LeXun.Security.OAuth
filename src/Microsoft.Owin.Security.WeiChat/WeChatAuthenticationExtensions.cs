using Microsoft.Owin.Security;
using Microsoft.Owin.Security.WeiChat;
using System;

namespace Owin
{
    public static class WeiChatAuthenticationExtensions
    {
        //public static void UseWeiChatAuthentication(this IAppBuilder app, WeiChatAuthenticationOptions options)
        //{
        //    if (app == null)
        //    {
        //        throw new ArgumentNullException("app");
        //    }
        //    if (options == null)
        //    {
        //        throw new ArgumentNullException("options");
        //    }
        //    app.Use(typeof(WeiChatAuthenticationMiddleware), app, options);
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IAppBuilder UseWeiChatAuthentication(this IAppBuilder app, WeiChatAuthenticationOptions options)
        {
            if (app == null) throw new ArgumentNullException("app");
            if (options == null) throw new ArgumentNullException("options");

            app.Use(typeof(WeiChatAuthenticationMiddleware), app, options);

            return app;
        }

        //public static void UseWeiChatAuthentication(this IAppBuilder app, string appId, string appSecret)
        //{
        //    UseWeiChatAuthentication(app, new WeiChatAuthenticationOptions()
        //    {
        //        AppId = appId,
        //        AppSecret = appSecret,
        //        Schema = "https"
        //    });
        //}

        ///// <summary>
        ///// 微信授权登录
        ///// </summary>
        ///// <param name="app"></param>
        ///// <param name="appId"></param>
        ///// <param name="appSecret"></param>
        ///// <returns></returns>
        //public static IAppBuilder UseWeiChatAuthentication(this IAppBuilder app, string appId, string appSecret)
        //{
        //    return app.UseWeiChatAuthentication(new WeiChatAuthenticationOptions
        //    {
        //        AppId = appId,
        //        AppSecret = appSecret,
        //        Schema = "https"
        //    });
        //}

        //public static void UseWeiChatAuthentication(this IAppBuilder app, string appId, string appSecret)
        //{
        //    UseWeiChatAuthentication(app, new WeiChatAuthenticationOptions()
        //    {
        //        AppId = appId,
        //        AppSecret = appSecret,
        //        SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType()
        //    });
        //}

        /// <summary>
        /// 微信授权登录
        /// </summary>
        /// <param name="app"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static IAppBuilder UseWeiChatAuthentication(this IAppBuilder app, string appId, string appSecret)
        {
            return app.UseWeiChatAuthentication(new WeiChatAuthenticationOptions
            {
                AppId = appId,
                AppSecret = appSecret,
                SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType()
            });
        }
    }
}