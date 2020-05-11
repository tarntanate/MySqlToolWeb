using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsByNameAdvertiser
{
    public class IsExistsByNameAdvertiserCommand : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsByNameAdvertiserCommand(string name)
        {
            Name = name;
        }
    }
}
