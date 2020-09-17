using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusCommand : UpdateAdStatusRequest, IRequest<HttpResult<bool>>
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
