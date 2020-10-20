using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupIdListByPublisherIdRedis
{
    public class GetAdGroupIdListByPublisherIdRedisQuery : IRequest<Response<string>>
    {
        public string PublisherName { get; set; }

        public GetAdGroupIdListByPublisherIdRedisQuery(string publisherName)
        {
            PublisherName = publisherName;
        }
    }
}
