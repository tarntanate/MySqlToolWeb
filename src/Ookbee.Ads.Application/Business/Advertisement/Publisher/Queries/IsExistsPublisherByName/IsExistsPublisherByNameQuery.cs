using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Queries.IsExistsPublisherByName
{
    public class IsExistsPublisherByNameQuery : IRequest<Response<bool>>
    {
        public string Name { get; set; }

        public IsExistsPublisherByNameQuery(string name)
        {
            Name = name;
        }
    }
}
