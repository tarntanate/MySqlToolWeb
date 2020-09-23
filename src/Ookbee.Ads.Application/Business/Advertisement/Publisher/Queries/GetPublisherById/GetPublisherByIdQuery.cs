using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Queries.GetPublisherById
{
    public class GetPublisherByIdQuery : IRequest<Response<PublisherDto>>
    {
        public long Id { get; set; }

        public GetPublisherByIdQuery(long id)
        {
            Id = id;
        }
    }
}
