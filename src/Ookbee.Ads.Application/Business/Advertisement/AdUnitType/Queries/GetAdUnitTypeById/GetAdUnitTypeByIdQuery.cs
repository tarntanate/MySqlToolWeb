using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.GetAdUnitTypeById
{
    public class GetAdUnitTypeByIdQuery : IRequest<HttpResult<AdUnitTypeDto>>
    {
        public long Id { get; set; }

        public GetAdUnitTypeByIdQuery(long id)
        {
            Id = id;
        }
    }
}
