using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Cache.Commands.CacheInitialization
{
    public class CacheInitializationCommand : IRequest<Unit>
    {
        public CacheInitializationCommand()
        {

        }
    }
}
