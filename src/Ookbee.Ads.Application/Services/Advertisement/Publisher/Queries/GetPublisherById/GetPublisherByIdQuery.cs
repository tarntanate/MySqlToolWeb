using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherById
{
    public class GetPublisherByIdQuery : IRequest<Response<PublisherDto>>
    {
        public long Id { get; private set; }

        public GetPublisherByIdQuery(long id)
        {
            Id = id;
        }
    }
}
