using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById
{
    public class IsExistsAdByIdQuery : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsAdByIdQuery(string id)
        {
            Id = id;
        }
    }
}
