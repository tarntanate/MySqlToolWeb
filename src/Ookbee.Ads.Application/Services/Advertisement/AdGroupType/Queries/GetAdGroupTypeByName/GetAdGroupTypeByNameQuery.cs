using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.GetAdGroupTypeByName
{
    public class GetAdGroupTypeByNameQuery : IRequest<Response<AdGroupTypeDto>>
    {
        public string Name { get; private set; }

        public GetAdGroupTypeByNameQuery(string name)
        {
            Name = name;
        }
    }
}
