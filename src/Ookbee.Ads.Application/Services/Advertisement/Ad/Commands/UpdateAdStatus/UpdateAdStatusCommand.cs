using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusCommand : UpdateAdStatusRequest, IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public UpdateAdStatusCommand(long id, UpdateAdStatusRequest request)
        {
            Id = id;
            Status = request.Status;
        }
    }
}
