using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheAdById;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheAdIdListByPlatform;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheAdByUnitId
{
    public class CreateCacheAdByUnitIdCommandHandler : IRequestHandler<CreateCacheAdByUnitIdCommand, Unit>
    {
        private IMediator Mediator { get; }

        public CreateCacheAdByUnitIdCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(CreateCacheAdByUnitIdCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdList = await Mediator.Send(new GetAdListQuery(start, length, request.AdUnitId, null));
                if (getAdList.Ok)
                {
                    foreach (var ad in getAdList.Data)
                    {
                        await Mediator.Send(new CreateCacheAdByIdCommand(ad.Id));
                        await Mediator.Send(new CreateCacheAdIdListByPlatformCommand(ad.Id, request.AdUnitId, ad.Platforms));
                    }
                    next = getAdList.Data.Count() == length ? true : false;
                    start += length;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
