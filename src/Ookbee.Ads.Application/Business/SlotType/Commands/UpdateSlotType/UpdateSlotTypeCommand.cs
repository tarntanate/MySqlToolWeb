using System;
using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.SlotType.Commands.UpdateSlotType
{
    public class UpdateSlotTypeCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime UpdatedAt => MechineDateTime.Now.DateTime;

        public UpdateSlotTypeCommand()
        {
            
        }

        public UpdateSlotTypeCommand(string id, UpdateSlotTypeCommand request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
