using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeByName
{
    public class IsExistsSlotTypeByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsSlotTypeByNameQuery(string name)
        {
            Name = name;
        }
    }
}
