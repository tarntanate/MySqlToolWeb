using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeCommand : CreateAdUnitTypeRequest, IRequest<HttpResult<long>>
    {
        public CreateAdUnitTypeCommand(CreateAdUnitTypeRequest request)
        {
            Name = request.Name;
            Description = request.Description;
        }
    }
}
