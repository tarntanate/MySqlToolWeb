using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitByName
{
    public class IsExistsAdUnitByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsAdUnitByNameQuery(string name)
        {
            Name = name;
        }
    }
}
