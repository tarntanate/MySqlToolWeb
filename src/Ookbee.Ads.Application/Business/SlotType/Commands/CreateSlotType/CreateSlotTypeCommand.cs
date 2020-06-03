using System;
using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.SlotType.Commands.CreateSlotType
{
    public class CreateSlotTypeCommand : IRequest<HttpResult<string>>
    {
        public string Id => ObjectId.GenerateNewId().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? CreatedAt => MechineDateTime.Now.DateTime;

        public CreateSlotTypeCommand()
        {
            
        }

        public CreateSlotTypeCommand(CreateSlotTypeCommand request)
        {
            Name = request.Name;
            Description = request.Description;
            ImageUrl = request.ImageUrl;
        }
    }
}
