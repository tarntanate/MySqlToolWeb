using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Publisher.Queries.GetPublisherByName
{
    public class GetPublisherByNameQuery : IRequest<HttpResult<PublisherDto>>
    {
        public string Name { get; set; }

        public GetPublisherByNameQuery(string name)
        {
            Name = name;
        }
    }
}
