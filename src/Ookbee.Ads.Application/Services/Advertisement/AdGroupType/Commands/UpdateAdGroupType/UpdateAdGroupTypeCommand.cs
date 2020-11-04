using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.UpdateAdGroupType
{
    public class UpdateAdGroupTypeCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public UpdateAdGroupTypeCommand(long id, UpdateAdGroupTypeRequest request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
