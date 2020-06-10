using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeByName
{
    public class GetAdUnitTypeByNameQuery : IRequest<HttpResult<AdUnitTypeDto>>
    {
        public string Name { get; set; }

        public GetAdUnitTypeByNameQuery(string name)
        {
            Name = name;
        }
    }
}
