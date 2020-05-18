using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeByName
{
    public class GetSlotTypeByNameQuery : IRequest<HttpResult<SlotTypeDto>>
    {
        public string Name { get; set; }

        public GetSlotTypeByNameQuery(string name)
        {
            Name = name;
        }
    }
}
