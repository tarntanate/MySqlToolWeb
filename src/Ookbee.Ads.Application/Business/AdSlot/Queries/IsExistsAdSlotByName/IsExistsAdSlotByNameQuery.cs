using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotByName
{
    public class IsExistsAdSlotByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsAdSlotByNameQuery(string name)
        {
            Name = name;
        }
    }
}
