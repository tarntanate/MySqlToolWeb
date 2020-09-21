using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.GetAdUnitById
{
    public class GetAdUnitByIdQuery : IRequest<HttpResult<AdUnitDto>>
    {
        public long Id { get; set; }

        public GetAdUnitByIdQuery(long id)
        {
            Id = id;
        }
    }
}
