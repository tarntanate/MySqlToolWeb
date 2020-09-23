using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.IsExistsAdGroupByName
{
    public class IsExistsAdGroupByNameQuery : IRequest<Response<bool>>
    {
        public string Name { get; set; }

        public IsExistsAdGroupByNameQuery(string name)
        {
            Name = name;
        }
    }
}
