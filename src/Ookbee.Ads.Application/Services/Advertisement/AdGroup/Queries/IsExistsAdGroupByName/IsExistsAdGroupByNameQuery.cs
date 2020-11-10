using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupByName
{
    public class IsExistsAdGroupByNameQuery : IRequest<Response<bool>>
    {
        public long PublisherId { get; private set; }
        public string Name { get; private set; }

        public IsExistsAdGroupByNameQuery(long publisherId, string name)
        {
            PublisherId = publisherId;
            Name = name;
        }
    }
}
