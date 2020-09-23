using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Commands.DeleteAdUnitType
{
    public class DeleteAdUnitTypeCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public DeleteAdUnitTypeCommand(long id)
        {
            Id = id;
        }
    }
}
