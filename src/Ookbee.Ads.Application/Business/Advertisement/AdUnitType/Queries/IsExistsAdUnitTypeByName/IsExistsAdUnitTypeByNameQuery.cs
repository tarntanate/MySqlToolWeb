using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.IsExistsAdUnitTypeByName
{
    public class IsExistsAdUnitTypeByNameQuery : IRequest<Response<bool>>
    {
        public string Name { get; set; }

        public IsExistsAdUnitTypeByNameQuery(string name)
        {
            Name = name;
        }
    }
}
