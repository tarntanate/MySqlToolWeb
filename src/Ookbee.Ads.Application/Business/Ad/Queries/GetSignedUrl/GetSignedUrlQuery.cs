using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetSignedUrl
{
    public class GetSignedUrlQuery : IRequest<HttpResult<string>>
    {
        public string Id { get; set; }

        public string CampaignId { get; set; }
        
        public string FileExtension { get; set; }

        public GetSignedUrlQuery(string campaignId, string id, GetSignedUrlQuery request)
        {
            Id = id;
            CampaignId = campaignId;
            FileExtension = request.FileExtension;
        }
    }
}
