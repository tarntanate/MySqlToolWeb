using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.DeleteAdUnit
{
    public class DeleteAdUnitCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public DeleteAdUnitCommand(long id)
        {
            Id = id;
        }
    }
}
