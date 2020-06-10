using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.UpdateAdUnitType
{
    public class UpdateAdUnitTypeCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UpdateAdUnitTypeCommand()
        {
            
        }

        public UpdateAdUnitTypeCommand(long id, UpdateAdUnitTypeCommand request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
