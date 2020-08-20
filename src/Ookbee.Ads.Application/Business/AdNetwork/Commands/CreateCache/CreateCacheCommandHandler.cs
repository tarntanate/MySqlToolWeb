using MediatR;
using Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheUnitListByGroupId;
using Ookbee.Ads.Common.Result;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCache
{
    public class CreateCacheCommandHandler : IRequestHandler<CreateCacheCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }

        public CreateCacheCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<HttpResult<bool>> Handle(CreateCacheCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdGroupList = await Mediator.Send(new GetAdGroupListQuery(0, 100, null, null));
                if (getAdGroupList.Ok)
                {
                    foreach (var adGroup in getAdGroupList.Data)
                    {
                        await Mediator.Send(new CreateCacheUnitListByGroupIdCommand(adGroup.Id));
                    }
                }
                next = getAdGroupList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);

            return result.Success(true);
        }
    }
}
