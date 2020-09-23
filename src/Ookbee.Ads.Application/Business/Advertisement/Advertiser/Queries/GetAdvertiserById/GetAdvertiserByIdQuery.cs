using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Advertiser.Queries.GetAdvertiserById
{
    public class GetAdvertiserByIdQuery : IRequest<Response<AdvertiserDto>>
    {
        public long Id { get; set; }

        public GetAdvertiserByIdQuery(long id)
        {
            Id = id;
        }
    }
}
