using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Advertisement.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusCommand : UpdateAdStatusRequest, IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public UpdateAdStatusCommand(long id, AdStatus status)
        {
            Id = id;
            Status = status;
        }

        public UpdateAdStatusCommand(long id, UpdateAdStatusRequest request)
        {
            Id = id;
            Status = request.Status;
        }
    }
}
