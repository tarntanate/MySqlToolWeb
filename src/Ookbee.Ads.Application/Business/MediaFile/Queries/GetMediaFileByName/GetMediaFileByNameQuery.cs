using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByName
{
    public class GetMediaFileByNameQuery : IRequest<HttpResult<MediaFileDto>>
    {
        public string AdId { get; set; }

        public string Name { get; set; }

        public GetMediaFileByNameQuery()
        {
            
        }

        public GetMediaFileByNameQuery(string adId, string name)
        {
            AdId = adId;
            Name = name;
        }
    }
}
