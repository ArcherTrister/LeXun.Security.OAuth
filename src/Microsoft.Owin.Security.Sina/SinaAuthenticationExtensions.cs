using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Sina;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin
{
    public static class SinaAuthenticationExtensions
    {
        public static IAppBuilder UseSinaAuthentication(this IAppBuilder app, SinaAuthenticationOptions options)
        { 
            if (app == null) throw new ArgumentNullException("app");
            if (options == null) throw new ArgumentNullException("options");

            app.Use(typeof(SinaAuthenticationMiddleware), app, options);

            return app;
        }

        //public static IAppBuilder UseSinaAuthentication(this IAppBuilder app, string appId, string appSecret)
        //{
        //    return app.UseSinaAuthentication(new SinaAuthenticationOptions
        //    {
        //        AppId = appId,
        //        AppSecret = appSecret,
        //        Schema = "https"
        //    });
        //}

        /// <summary>
        /// 新浪微博授权登录
        /// </summary>
        /// <param name="app"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static IAppBuilder UseSinaAuthentication(this IAppBuilder app, string appId, string appSecret)
        {
            return app.UseSinaAuthentication(new SinaAuthenticationOptions
            {
                AppId = appId,
                AppSecret = appSecret,
                SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType()
            });
        }
    }
}
