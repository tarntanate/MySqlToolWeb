using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupByName
{
    public class IsExistsAdGroupByNameQuery : IRequest<Response<bool>>
    {
        public string Name { get; private set; }

        public IsExistsAdGroupByNameQuery(string name)
        {
            Name = name;
        }
    }
}
