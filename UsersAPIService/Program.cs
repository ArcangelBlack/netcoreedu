using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace UsersAPIService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            //BuildWebHost(args).Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        //    public static IWebHost BuildWebHost(string[] args) =>
        //WebHost.CreateDefaultBuilder(args)
        //    .UseStartup<Startup>()
        //    .UseUrls("http://localhost:50925/")
        //    .Build();
    }
}
