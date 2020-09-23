using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdById
{
    public class GetAdByIdQuery : IRequest<Response<AdDto>>
    {
        public long Id { get; set; }

        public GetAdByIdQuery(long id)
        {
            Id = id;
        }
    }
}
