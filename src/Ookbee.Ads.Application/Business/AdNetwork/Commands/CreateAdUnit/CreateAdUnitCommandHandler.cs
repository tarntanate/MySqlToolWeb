using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdIdList;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdUnitById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdUnit
{
    public class CreateAdUnitCommandHandler : IRequestHandler<CreateAdUnitCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }
        private IDatabase AdsRedis { get; }

        public CreateAdUnitCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
            AdsRedis = adsRedis.Database();
        }

        public async Task<HttpResult<bool>> Handle(CreateAdUnitCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var start = 0;
            var length = 100;
            var next = true;

            do
            {
                var getAdList = await Mediator.Send(new GetAdIdListQuery(start, length, null, null));
                if (getAdList.Ok)
                {
                    foreach (var adId in getAdList.Data)
                    {
                        await Mediator.Send(new CreateAdUnitByIdCommand(adId));
                    }
                }
                next = getAdList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);

            return result.Success(true);

        }
    }
}
