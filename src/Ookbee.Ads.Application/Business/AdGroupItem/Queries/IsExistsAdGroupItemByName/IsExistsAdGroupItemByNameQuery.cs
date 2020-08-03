using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.IsExistsAdGroupItemByName
{
    public class IsExistsAdGroupItemByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsAdGroupItemByNameQuery(string name)
        {
            Name = name;
        }
    }
}
