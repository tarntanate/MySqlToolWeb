using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.CreateAdGroupCache;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class InitialAdNetworkService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public InitialAdNetworkService(IServiceProvider serviceProvider)
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
                    var start = 0;
                    var length = 100;
                    var next = true;
                    do
                    {
                        var getAdGroupList = await mediator.Send(new GetAdGroupListQuery(start, length, null, null), cancellationToken);
                        if (getAdGroupList.Ok)
                        {
                            foreach (var adGroup in getAdGroupList.Data)
                            {
                                await mediator.Send(new CreateAdGroupCacheCommand(adGroup.Id), cancellationToken);
                            }
                            start += length;
                        }
                        next = getAdGroupList.Data.Count() < length ? false : true;
                    }
                    while (next);
                }
            });
        }
    }
}
