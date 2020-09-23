using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherByName
{
    public class GetPublisherByNameQuery : IRequest<Response<PublisherDto>>
    {
        public string Name { get; private set; }

        public GetPublisherByNameQuery(string name)
        {
            Name = name;
        }
    }
}
