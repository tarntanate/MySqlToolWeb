using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnitType.Queries.IsExistsAdUnitTypeByName
{
    public class IsExistsAdUnitTypeByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsAdUnitTypeByNameQuery(string name)
        {
            Name = name;
        }
    }
}
