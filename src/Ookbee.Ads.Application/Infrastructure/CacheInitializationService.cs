using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Business.AdNetwork.Group.Commands.CreateGroupListByKey;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitList;
using System;
using System.Linq;
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
                        var start = 0;
                        var length = 100;
                        var isExits = true;
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        do
                        {
                            var getAdUnitList = await mediator.Send(new GetAdUnitListQuery(start, length, null), cancellationToken);
                            if (getAdUnitList.Ok)
                            {
                                foreach (var group in getAdUnitList.Data)
                                {
                                    await mediator.Send(new CreateGroupListByKeyCommand(group.Id));
                                }
                            }
                            start += length;
                            isExits = getAdUnitList.Data.Count() == length ? true : false;
                        }
                        while (isExits);
                    }
                }
                catch (OperationCanceledException) { }
            });
        }
    }
}