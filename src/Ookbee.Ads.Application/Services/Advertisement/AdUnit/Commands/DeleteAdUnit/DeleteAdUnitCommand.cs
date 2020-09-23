using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.DeleteAdUnit
{
    public class DeleteAdUnitCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public DeleteAdUnitCommand(long id)
        {
            Id = id;
        }
    }
}
