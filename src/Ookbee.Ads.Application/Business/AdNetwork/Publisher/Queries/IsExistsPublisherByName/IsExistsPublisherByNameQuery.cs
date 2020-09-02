using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Publisher.Queries.IsExistsPublisherByName
{
    public class IsExistsPublisherByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsPublisherByNameQuery(string name)
        {
            Name = name;
        }
    }
}
