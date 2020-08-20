using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CacheClear
{
    public class CacheClearCommand : IRequest<HttpResult<bool>>
    {

    }
}
