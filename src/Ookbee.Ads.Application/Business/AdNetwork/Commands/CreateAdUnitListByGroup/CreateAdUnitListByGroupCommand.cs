using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdUnitListByGroup
{
    public class CreateAdUnitListByGroupCommand : IRequest<HttpResult<bool>>
    {
        public CreateAdUnitListByGroupCommand()
        {

        }
    }
}
