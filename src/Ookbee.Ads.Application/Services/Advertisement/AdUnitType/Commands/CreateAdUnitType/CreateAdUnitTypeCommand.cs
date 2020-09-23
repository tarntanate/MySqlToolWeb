using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeCommand : IRequest<Response<long>>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public CreateAdUnitTypeCommand(CreateAdUnitTypeRequest request)
        {
            Name = request.Name;
            Description = request.Description;
        }
    }
}
