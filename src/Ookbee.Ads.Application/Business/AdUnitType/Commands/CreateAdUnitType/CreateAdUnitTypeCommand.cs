using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeCommand : IRequest<HttpResult<long>>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateAdUnitTypeCommand()
        {

        }

        public CreateAdUnitTypeCommand(CreateAdUnitTypeCommand request)
        {
            Name = request.Name;
            Description = request.Description;
        }
    }
}
