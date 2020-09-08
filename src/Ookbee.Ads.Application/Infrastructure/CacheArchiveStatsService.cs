using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Business.Cache.ArchiveStatsCache;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class CacheArchiveStatsService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public CacheArchiveStatsService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(async () =>
            {
                try
                {
                    using (var scope = ServiceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        await mediator.Send(new ArchiveStatsCacheCommand(), cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}