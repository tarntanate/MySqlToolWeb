using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.DeleteMediaFile
{
    public class DeleteMediaFileCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string CampaignId { get; set; }

        public string AdId { get; set; }

        public DeleteMediaFileCommand(string campaignId, string adId, string id)
        {
            Id = id;
            CampaignId = campaignId;
            AdId = adId;
        }
    }
}
