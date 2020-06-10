using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }
        public long AdUnitTypeId { get; set; }
        public long PublisherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UpdateAdUnitCommand()
        {
            
        }

        public UpdateAdUnitCommand(long id, UpdateAdUnitCommand request)
        {
            Id = id;
            AdUnitTypeId = request.AdUnitTypeId;
            PublisherId = request.PublisherId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
