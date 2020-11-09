using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupByName
{
    public class GetAdGroupByNameQuery : IRequest<Response<AdGroupDto>>
    {
        public long PublisherId { get; private set; }
        public string Name { get; private set; }

        public GetAdGroupByNameQuery(long publisherId, string name)
        {
            PublisherId = publisherId;
            Name = name;
        }
    }
}
