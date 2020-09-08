using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Business.Cache.Commands.InitialAdCache;
using Ookbee.Ads.Application.Business.Cache.Commands.InitialStatsCache;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class CacheInitialService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public CacheInitialService(IServiceProvider serviceProvider)
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
                        await mediator.Send(new InitialAdCacheCommand(), cancellationToken);
                        await mediator.Send(new InitialStatsCacheCommand(), cancellationToken);
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