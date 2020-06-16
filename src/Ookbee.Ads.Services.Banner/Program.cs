using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Infrastructure;

namespace Ookbee.Ads.Services.Banner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppBuilder.CreateDefaultBuilder(args)
                      .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                      .Build()
                      .Run();
        }
    }
}
