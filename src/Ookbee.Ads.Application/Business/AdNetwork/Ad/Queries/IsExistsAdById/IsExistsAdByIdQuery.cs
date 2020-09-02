using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.IsExistsAdById
{
    public class IsExistsAdByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdByIdQuery(long id)
        {
            Id = id;
        }
    }
}
