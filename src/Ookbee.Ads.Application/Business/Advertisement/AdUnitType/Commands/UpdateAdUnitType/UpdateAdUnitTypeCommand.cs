using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Commands.UpdateAdUnitType
{
    public class UpdateAdUnitTypeCommand : UpdateAdUnitTypeRequest, IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public UpdateAdUnitTypeCommand(long id, UpdateAdUnitTypeRequest request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
