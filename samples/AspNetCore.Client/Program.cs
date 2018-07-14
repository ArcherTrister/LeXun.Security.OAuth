using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AspNetCore.Client
{
    public class Program
    {
        #region 2.0

        //public static void Main(string[] args)
        //{
        //    BuildWebHost(args).Run();
        //}

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //    .UseKestrel()
        //        .UseIISIntegration()
        //        .UseStartup<Startup>()
        //        .Build();

        #endregion

        #region 2.1

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        #endregion

    }
}
