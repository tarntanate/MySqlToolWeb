using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.DeleteAdUnit
{
    public class DeleteAdUnitCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteAdUnitCommand(long id)
        {
            Id = id;
        }
    }
}
