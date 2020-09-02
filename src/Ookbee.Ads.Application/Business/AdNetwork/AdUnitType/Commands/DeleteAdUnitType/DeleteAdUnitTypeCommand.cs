using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnitType.Commands.DeleteAdUnitType
{
    public class DeleteAdUnitTypeCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteAdUnitTypeCommand(long id)
        {
            Id = id;
        }
    }
}
