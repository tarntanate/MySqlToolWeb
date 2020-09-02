using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Publisher.Queries.GetPublisherById
{
    public class GetPublisherByIdQuery : IRequest<HttpResult<PublisherDto>>
    {
        public long Id { get; set; }

        public GetPublisherByIdQuery(long id)
        {
            Id = id;
        }
    }
}
