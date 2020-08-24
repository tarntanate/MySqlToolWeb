using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.ClearCache
{
    public class ClearCacheCommand : IRequest<HttpResult<bool>>
    {

    }
}
