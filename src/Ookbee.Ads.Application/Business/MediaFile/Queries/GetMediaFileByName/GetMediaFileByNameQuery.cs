using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByName
{
    public class GetMediaFileByNameQuery : IRequest<HttpResult<MediaFileDto>>
    {
        public string CampaignId { get; set; }
        
        public string AdId { get; set; }

        public string Name { get; set; }

        public GetMediaFileByNameQuery(string campaignId, string adId, string name)
        {
            CampaignId = campaignId;
            AdId = adId;
            Name = name;
        }
    }
}
