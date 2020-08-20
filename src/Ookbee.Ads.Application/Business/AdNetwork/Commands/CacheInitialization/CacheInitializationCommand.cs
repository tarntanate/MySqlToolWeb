using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CacheInitialization
{
    public class CacheInitializationCommand : IRequest<HttpResult<bool>>
    {
        public CacheInitializationCommand()
        {

        }
    }
}
