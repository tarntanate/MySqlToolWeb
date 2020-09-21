using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Queries.IsExistsPublisherByName
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
