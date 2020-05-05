using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Commands.UpdateCampaignItemType
{
    public class UpdateCampaignItemTypeCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public UpdateCampaignItemTypeCommand()
        {
            Id = string.Empty;
        }

        public UpdateCampaignItemTypeCommand(string id, UpdateCampaignItemTypeCommand request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
            ImageUrl = request.ImageUrl;
        }
    }
}
