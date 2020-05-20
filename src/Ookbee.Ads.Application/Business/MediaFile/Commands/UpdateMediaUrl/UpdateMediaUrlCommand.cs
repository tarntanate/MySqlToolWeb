using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaUrl
{
    public class UpdateMediaUrlCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string CampaignId { get; set; }

        public string AdId { get; set; }
        
        public string MediaUrl { get; set; }

        public UpdateMediaUrlCommand()
        {
            
        }

        public UpdateMediaUrlCommand(string adId, string id, string mediaUrl)
        {
            Id = id;
            AdId = adId;
            MediaUrl = mediaUrl;
        }
    }
}
