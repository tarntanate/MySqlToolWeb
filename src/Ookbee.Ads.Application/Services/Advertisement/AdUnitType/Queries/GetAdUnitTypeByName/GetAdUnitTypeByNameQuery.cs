using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Queries.GetAdUnitTypeByName
{
    public class GetAdUnitTypeByNameQuery : IRequest<Response<AdUnitTypeDto>>
    {
        public string Name { get; set; }

        public GetAdUnitTypeByNameQuery(string name)
        {
            Name = name;
        }
    }
}
