using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UnitType.Queries.GetByIdUnitType
{
    public class GetByIdUnitTypeCommand : IRequest<HttpResult<UnitTypeDto>>
    {
        public string Id { get; set; }

        public GetByIdUnitTypeCommand(string id)
        {
            Id = id;
        }
    }
}
