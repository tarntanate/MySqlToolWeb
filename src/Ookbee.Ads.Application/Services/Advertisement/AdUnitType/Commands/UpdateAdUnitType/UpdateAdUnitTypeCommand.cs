using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Commands.UpdateAdUnitType
{
    public class UpdateAdUnitTypeCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public UpdateAdUnitTypeCommand(long id, UpdateAdUnitTypeRequest request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
