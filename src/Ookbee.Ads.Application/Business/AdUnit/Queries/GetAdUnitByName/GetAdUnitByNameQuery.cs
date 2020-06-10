using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitByName
{
    public class GetAdUnitByNameQuery : IRequest<HttpResult<AdUnitDto>>
    {
        public string Name { get; set; }

        public GetAdUnitByNameQuery(string name)
        {
            Name = name;
        }
    }
}
