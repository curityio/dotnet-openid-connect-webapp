using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OidcClientDemoApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    
                    /* Get this working and show how to use SSL
                    webBuilder.UseKestrel(options => {
                        options.Listen(IPAddress.Any, 5000);
                    });*/
                });
    }
}
