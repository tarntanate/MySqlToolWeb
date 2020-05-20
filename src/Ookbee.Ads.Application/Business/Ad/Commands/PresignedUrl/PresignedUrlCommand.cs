using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Commands.PresignedUrl
{
    public class PresignedUrlCommand : IRequest<HttpResult<string>>
    {
        public string Id { get; set; }

        public string CampaignId { get; set; }
        
        public string FileExtension { get; set; }

        public PresignedUrlCommand()
        {
            
        }

        public PresignedUrlCommand(string campaignId, string id, PresignedUrlCommand request)
        {
            Id = id;
            CampaignId = campaignId;
            FileExtension = request.FileExtension;
        }
    }
}
