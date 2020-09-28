using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Services.Cache.AdUserCache.Commands.InitialAdUserCache;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class InitialAdUsrPreviewService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public InitialAdUsrPreviewService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(async () =>
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var next = true;
                    do
                    {
                        await mediator.Send(new InitialAdUserCacheCommand(), cancellationToken);
                        var nowDateTime = MechineDateTime.Now;
                        var nextDateTime = nowDateTime.RoundUp(TimeSpan.FromDays(1));
                        var timeout = nextDateTime - nowDateTime;
                        Thread.Sleep(timeout);
                    }
                    while (next);
                }
            });
        }
    }
}
