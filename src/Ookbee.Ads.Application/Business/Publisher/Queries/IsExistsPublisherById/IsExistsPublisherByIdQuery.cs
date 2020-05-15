using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById
{
    public class IsExistsPublisherByIdQuery : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsPublisherByIdQuery(string id)
        {
            Id = id;
        }
    }
}
