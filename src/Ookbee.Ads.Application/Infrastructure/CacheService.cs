using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.AdNetwork.GroupItem.Commands.CreateGroupItemListByKey;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class CacheService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }

        public CacheService(IServiceProvider serviceProvider)
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
                        var start = 0;
                        var length = 100;
                        var isExits = true;
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        do
                        {
                            var getAdGroupList = await mediator.Send(new GetAdGroupListQuery(start, length), cancellationToken);
                            if (getAdGroupList.Ok)
                            {
                                foreach (var group in getAdGroupList.Data)
                                {
                                    await mediator.Send(new CreateGroupItemListByKeyCommand(group.Id));
                                }
                            }
                            start += length;
                            isExits = getAdGroupList.Data.Count() == length ? true : false;
                        }
                        while (isExits);
                    }
                }
                catch (OperationCanceledException) { }
            });
        }
    }
}