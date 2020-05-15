using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdById
{
    public class GetAdByIdQuery : IRequest<HttpResult<AdDto>>
    {
        public string Id { get; set; }

        public GetAdByIdQuery(string id)
        {
            Id = id;
        }
    }
}
