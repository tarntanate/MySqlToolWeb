using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsByIdSlotType
{
    public class IsExistsByIdSlotTypeCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdSlotTypeCommand(string id)
        {
            Id = id;
        }
    }
}
