using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.DeleteAdGroupItem
{
    public class DeleteAdGroupItemCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteAdGroupItemCommand(long id)
        {
            Id = id;
        }
    }
}
