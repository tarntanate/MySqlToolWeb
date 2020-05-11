using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UnitType.Queries.IsExistsByIdUnitType
{
    public class IsExistsByIdUnitTypeCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdUnitTypeCommand(string id)
        {
            Id = id;
        }
    }
}
