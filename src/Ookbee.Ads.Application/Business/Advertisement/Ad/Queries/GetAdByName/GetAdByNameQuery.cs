using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Ad.Queries.GetAdByName
{
    public class GetAdByNameQuery : IRequest<Response<AdDto>>
    {
        public string Name { get; set; }

        public GetAdByNameQuery(string name)
        {
            Name = name;
        }
    }
}
