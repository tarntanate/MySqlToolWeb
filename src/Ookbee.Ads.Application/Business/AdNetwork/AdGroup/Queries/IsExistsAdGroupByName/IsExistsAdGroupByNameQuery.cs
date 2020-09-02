using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.IsExistsAdGroupByName
{
    public class IsExistsAdGroupByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsAdGroupByNameQuery(string name)
        {
            Name = name;
        }
    }
}
