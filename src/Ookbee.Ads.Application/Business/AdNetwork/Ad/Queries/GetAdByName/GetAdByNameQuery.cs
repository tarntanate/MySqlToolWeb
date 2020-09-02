using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdByName
{
    public class GetAdByNameQuery : IRequest<HttpResult<AdDto>>
    {
        public string Name { get; set; }

        public GetAdByNameQuery(string name)
        {
            Name = name;
        }
    }
}
