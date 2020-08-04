using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.UpdateAdGroupItem
{
    public class UpdateAdGroupItemCommand : UpdateAdGroupItemRequest, IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public UpdateAdGroupItemCommand(long id, UpdateAdGroupItemRequest request)
        {
            Id = id;
            AdGroupId = request.AdGroupId;
            AdUnitKey = request.AdUnitKey;
            Name = request.Name;
            Description = request.Description;
            SortSeq = request.SortSeq;
        }
    }
}
