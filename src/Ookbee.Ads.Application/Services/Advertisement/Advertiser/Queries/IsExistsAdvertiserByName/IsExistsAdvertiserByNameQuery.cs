using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Queries.IsExistsAdvertiserByName
{
    public class IsExistsAdvertiserByNameQuery : IRequest<Response<bool>>
    {
        public string Name { get; private set; }

        public IsExistsAdvertiserByNameQuery(string name)
        {
            Name = name;
        }
    }
}
