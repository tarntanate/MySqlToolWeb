using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Services.Cache.AdGroupCache.Commands.CreateAdGroupCache;
using Ookbee.Ads.Common.Extensions;
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
                        next = false;
                        var getAdGroupList = await mediator.Send(new GetAdGroupListQuery(start, length, null, null), cancellationToken);
                        if (getAdGroupList.Ok &&
                            getAdGroupList.Data.HasValue())
                        {
                            var items = getAdGroupList.Data;
                            foreach (var item in items)
                            {
                                await mediator.Send(new CreateAdGroupCacheCommand(item.Id), cancellationToken);
                            }
                            start += length;
                            next = items.Count() < length ? false : true;
                        }
                    }
                    while (next);
                }
            });
        }
    }
}
