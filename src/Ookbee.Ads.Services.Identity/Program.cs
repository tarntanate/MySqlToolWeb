using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Extensions.Builder;

namespace Ookbee.Ads.Services.Identity
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
