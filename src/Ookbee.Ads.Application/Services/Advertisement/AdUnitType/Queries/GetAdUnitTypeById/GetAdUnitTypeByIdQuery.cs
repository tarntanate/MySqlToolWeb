using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Queries.GetAdUnitTypeById
{
    public class GetAdUnitTypeByIdQuery : IRequest<Response<AdUnitTypeDto>>
    {
        public long Id { get; set; }

        public GetAdUnitTypeByIdQuery(long id)
        {
            Id = id;
        }
    }
}
