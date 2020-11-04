using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.CreateAdGroupType
{
    public class CreateAdGroupTypeCommand : IRequest<Response<long>>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public CreateAdGroupTypeCommand(CreateAdGroupTypeRequest request)
        {
            Name = request.Name;
            Description = request.Description;
        }
    }
}
