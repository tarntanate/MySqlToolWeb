using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupByName
{
    public class GetAdGroupByNameQuery : IRequest<Response<AdGroupDto>>
    {
        public string Name { get; set; }

        public GetAdGroupByNameQuery(string name)
        {
            Name = name;
        }
    }
}
