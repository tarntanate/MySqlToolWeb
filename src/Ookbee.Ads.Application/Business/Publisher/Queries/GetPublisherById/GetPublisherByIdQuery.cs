using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherById
{
    public class GetPublisherByIdQuery : IRequest<HttpResult<PublisherDto>>
    {
        public string Id { get; set; }

        public GetPublisherByIdQuery(string id)
        {
            Id = id;
        }
    }
}
