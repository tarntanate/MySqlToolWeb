using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Advertiser.Queries.IsExistsAdvertiserByName
{
    public class IsExistsAdvertiserByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsAdvertiserByNameQuery(string name)
        {
            Name = name;
        }
    }
}
