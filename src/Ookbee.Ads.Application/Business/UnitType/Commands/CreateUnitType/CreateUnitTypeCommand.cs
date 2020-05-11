using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UnitType.Commands.CreateUnitType
{
    public class CreateUnitTypeCommand : IRequest<HttpResult<string>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public CreateUnitTypeCommand()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public CreateUnitTypeCommand(CreateUnitTypeCommand request)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Name = request.Name;
            Description = request.Description;
            ImageUrl = request.ImageUrl;
        }
    }
}
