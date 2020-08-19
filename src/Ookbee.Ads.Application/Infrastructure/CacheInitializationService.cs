using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Business.AdNetworkGroup.Commands.CreateAdNetworkGroupCache;
using Ookbee.Ads.Application.Business.AdNetworkUnit.Commands.CreateAdNetworkUnitCache;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class CacheInitializationService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public CacheInitializationService(IServiceProvider serviceProvider)
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
                        await mediator.Send(new CreateAdNetworkGroupCacheCommand());
                        await mediator.Send(new CreateAdNetworkUnitCacheCommand());
                    }
                }
                catch (OperationCanceledException)
                {

                }
            });
        }
    }
}