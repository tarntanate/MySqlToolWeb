using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.Ad.Queries.IsExistsAdByName
{
    public class IsExistsAdByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsAdByNameQuery(string name)
        {
            Name = name;
        }
    }
}
