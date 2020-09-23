using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Queries.GetPublisherByName
{
    public class GetPublisherByNameQuery : IRequest<Response<PublisherDto>>
    {
        public string Name { get; set; }

        public GetPublisherByNameQuery(string name)
        {
            Name = name;
        }
    }
}
