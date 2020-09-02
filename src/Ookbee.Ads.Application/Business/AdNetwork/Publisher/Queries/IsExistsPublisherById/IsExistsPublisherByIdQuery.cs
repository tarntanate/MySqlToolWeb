using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Publisher.Queries.IsExistsPublisherById
{
    public class IsExistsPublisherByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsPublisherByIdQuery(long id)
        {
            Id = id;
        }
    }
}
