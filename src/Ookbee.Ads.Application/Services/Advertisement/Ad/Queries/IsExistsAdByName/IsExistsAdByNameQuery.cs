using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.IsExistsAdByName
{
    public class IsExistsAdByNameQuery : IRequest<Response<bool>>
    {
        public string Name { get; set; }

        public IsExistsAdByNameQuery(string name)
        {
            Name = name;
        }
    }
}
