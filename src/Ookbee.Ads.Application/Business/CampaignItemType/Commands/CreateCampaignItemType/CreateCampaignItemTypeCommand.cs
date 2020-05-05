using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Commands.CreateCampaignItemType
{
    public class CreateCampaignItemTypeCommand : IRequest<HttpResult<string>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public CreateCampaignItemTypeCommand()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public CreateCampaignItemTypeCommand(CreateCampaignItemTypeCommand request)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Name = request.Name;
            Description = request.Description;
            ImageUrl = request.ImageUrl;
        }
    }
}
