using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsByIdAdvertiser
{
    public class IsExistsByIdAdvertiserCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdAdvertiserCommand(string id)
        {
            Id = id;
        }
    }
}
