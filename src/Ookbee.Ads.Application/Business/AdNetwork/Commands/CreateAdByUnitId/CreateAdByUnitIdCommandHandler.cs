using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdById;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdIdListByPlatform;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdByUnitId
{
    public class CreateAdByUnitIdCommandHandler : IRequestHandler<CreateAdByUnitIdCommand, Unit>
    {
        private IMediator Mediator { get; }

        public CreateAdByUnitIdCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(CreateAdByUnitIdCommand request, CancellationToken cancellationToken)
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
                        await Mediator.Send(new CreateAdByIdCommand(ad.Id));
                        await Mediator.Send(new CreateAdIdListByPlatformCommand(ad.Id, request.AdUnitId, ad.Platforms));
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
