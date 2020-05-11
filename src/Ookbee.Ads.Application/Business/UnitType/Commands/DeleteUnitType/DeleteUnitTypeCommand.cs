using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UnitType.Commands.DeleteUnitType
{
    public class DeleteUnitTypeCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteUnitTypeCommand(string id)
        {
            Id = id;
        }
    }
}
