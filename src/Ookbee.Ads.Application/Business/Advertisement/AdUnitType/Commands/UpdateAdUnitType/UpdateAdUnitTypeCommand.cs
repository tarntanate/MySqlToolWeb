using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Commands.UpdateAdUnitType
{
    public class UpdateAdUnitTypeCommand : UpdateAdUnitTypeRequest, IRequest<HttpResult<bool>>
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
