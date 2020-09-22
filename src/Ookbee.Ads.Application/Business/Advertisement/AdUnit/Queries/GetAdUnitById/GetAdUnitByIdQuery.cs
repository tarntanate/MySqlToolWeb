using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.GetAdUnitById
{
    public class GetAdUnitByIdQuery : IRequest<Response<AdUnitDto>>
    {
        public long Id { get; set; }

        public GetAdUnitByIdQuery(long id)
        {
            Id = id;
        }
    }
}
