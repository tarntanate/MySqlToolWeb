using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeCommand : CreateAdUnitTypeRequest, IRequest<Response<long>>
    {
        public CreateAdUnitTypeCommand(CreateAdUnitTypeRequest request)
        {
            Name = request.Name;
            Description = request.Description;
        }
    }
}
