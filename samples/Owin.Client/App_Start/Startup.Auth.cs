using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Alipay;
using Microsoft.Owin.Security.Baidu;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.QQ;
using Microsoft.Owin.Security.WeiChat;
using Owin;
using Owin.Client.Models;

namespace Owin.Client
{
    public partial class Startup
    {
        // 有关配置身份验证的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // 配置数据库上下文、用户管理器和登录管理器，以便为每个请求使用单个实例
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // 使应用程序可以使用 Cookie 来存储已登录用户的信息
            // 并使用 Cookie 来临时存储有关使用第三方登录提供程序登录的用户的信息
            // 配置登录 Cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // 当用户登录时使应用程序可以验证安全戳。
                    // 这是一项安全功能，当你更改密码或者向帐户添加外部登录名时，将使用此功能。
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 使应用程序可以在双重身份验证过程中验证第二因素时暂时存储用户信息。
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // 使应用程序可以记住第二登录验证因素，例如电话或电子邮件。
            // 选中此选项后，登录过程中执行的第二个验证步骤将保存到你登录时所在的设备上。
            // 此选项类似于在登录时提供的“记住我”选项。
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // 取消注释以下行可允许使用第三方登录提供程序登录
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
            //app.UseBaiduAuthentication(new BaiduAuthenticationOptions()
            //{
            //    AppId = "",
            //    AppSecret = "",
            //    //CallbackPath = new PathString("")
            //});
            //app.UseQQAuthentication(new QQAuthenticationOptions()
            //{
            //    AppId = "",
            //    AppSecret = "",
            //    //CallbackPath = new PathString("")
            //});
            //app.UseWeiChatAuthentication(new WeiChatAuthenticationOptions()
            //{
            //    AppId = "",
            //    AppSecret = "",
            //    //CallbackPath = new PathString("")
            //});
            app.UseAlipayAuthentication(new AlipayAuthenticationOptions() {
                AppId = "2018020502144102",
                AppSecret = "MIIEpAIBAAKCAQEArvhcEe3uZbwbDXgE4GoZH2r5KHZ/5b1J19fMo2QGI8uhbXm+6fq+scsjF37TFvBhGeF1Elpvc9qcdLHy5IHErT5R8C4al91TKvbDLHvApFjaS/yNM8zlWH8rylr6TTtt/apt4j1sNZSFXjEQCvfOE09sznaGHZG26fmbT2KztdwHBv8HHtS1sUG66lQbd0emEdNKqiBsy1pasA9BisuiqPwcCwqU1aT9wy2BtW6h6yYsJwxdpDmQBjzfCXunTBn8PdhKCoarf3itYsPgu8FetRB5Db0jqA6to+w/i5EiO+9EF1OCWa3fJNfM82v20b1o3eVifea4a2lg0aoEfDovdQIDAQABAoIBAQCoacPtOhByegmsAC7pdxYxaHzklpLqpjTyffOp/Xfvcmwx/LJncoOkjHt9fQRfmwZLq/hMryGB6RUZOcMqcUHG4yppPWc5b75Cp3wbUA6P4jpUU4XyKzFB6j5TKxiryB1JwYa88hQ6yndIv37Kn9UKPzPi0rCqu3vNYAU+9yk1TDgrgTpmnrNxI9F48R7qWOg3dKJNc9N2GjI3jAXU97uQneqROsufYp1GHwEbW6HZ73iqtLS/IW+nrj0vjWALDMz2LOrfWBRaCLOQAjBxofVOn1ZvmwcJlNAdk8al4c5fF0pBtehts4tZs+e/T7yLA1j3qE/rfTk9cC8BRkmDnZbNAoGBAOPMkI+y6vQ73u4YF1rxN7WuEmwv4T6kn9AimvfCnXe6H28Iejgswv0ZPHu8EZpTh5IHglTit1oPRtXZHFYA+QjhBzQzoLbAt3vomFAcbBNRAcaLlbZZ5zJQHixogro19hn7v77j3CcUCFLhiAVqPkcTYBmvJJUGcC633qggEBiLAoGBAMShiM8jP9G7nQJtjqcUmQJOM1Rgzxt5s5R54EwyWLh542+oC/QHTkQ9PllI8Gi6k4IiTzpcYC3Ruxw8O34kz6WptqIe6ZwWbe1fWJLLVufnabt2EQwKzsoVr3dhuvzLyHfEUMXkiYOUOXX9jWvRr5Rqwq4AY/WAMtuhFoNEX9f/AoGBANo630LK5MuVj7wI2FcHP5eNa5i5Rc/9Zhy/CjbQ+on4hKSaOnwWZbaStp7TTQnLe9Up3HH1wDFG8VVs6Ph7dhhLe9tGmnB5r03FRiV5FRBsSocqsgI/nn1Uw5NHi/VYPKwnwUnegMnvJwo/hU+quH7e0PHKGAGsaUxYWbY3ATSBAoGAfzQ1CjO2jJr/pttzRrl3httKL5L1SMQBndL+fKyxYp5PuSMQoIy9YD3ygNZD2Kyi0rQZZxrtiZa9ojWBE0kDIBvbr6Op4zVLZh2hck2jaiD18LUfsBep0WgX/HY3/mRiysAwtOT05S9VwPQHsjGTkcNDMEfGYsLNo/HhW4g3LMcCgYAOudT5bdvXEarsPl1t9YLnLAf2YN86iryN38Pqzcq+djHqvqHnBIJFxKOXv3QGhEt5GUo1925joHndNjjC5ylqsONIg6zOG7zEh/d/N/Oxus6MwcwnFcKGYQ9XT4gxBsBfs2qyxvuLszC2+4cpcvVzVUmWj899ADk2ZyHr7cDfZg==",
                AlipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA1R5d26E8zyuNgekMvN6V6Yerc+8ElvereBoWOxl7g9M3CUWsQ5I78EOraA3PVv6PNxfTixaAZpxnO801cVN1X/UtE4UDchiD/1nk8C9gsJRjqfISi/De5vDpJeahY5i5n5GH/wBwJljqY/fDLvW1FVn0Xdo3n9X8q5ymc6b6UF1EgMMmBqcWD2CWG3I2COv45lHIDhGSOYCzVJ2SyKgYdaFIZiM5Skz4IxjCUI7hDFeMMl431//8WacA8C01bn8fLEI0iNGNrcKG5WBLt4nd4uasxRgkvouyQy4LCcHhbmcRzhIKxwASRkpZ7DBtrhesO3VdB1CPFDQAHexslQkC0wIDAQAB"
            });
        }
    }
}