using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.GetAdGroupTypeById
{
    public class GetAdGroupTypeByIdQuery : IRequest<Response<AdGroupTypeDto>>
    {
        public long Id { get; private set; }

        public GetAdGroupTypeByIdQuery(long id)
        {
            Id = id;
        }
    }
}
