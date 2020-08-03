using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemByName
{
    public class GetAdGroupItemByNameQuery : IRequest<HttpResult<AdGroupItemDto>>
    {
        public string Name { get; set; }

        public GetAdGroupItemByNameQuery(string name)
        {
            Name = name;
        }
    }
}
