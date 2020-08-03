using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupByName
{
    public class GetAdGroupByNameQuery : IRequest<HttpResult<AdGroupDto>>
    {
        public string Name { get; set; }

        public GetAdGroupByNameQuery(string name)
        {
            Name = name;
        }
    }
}
