using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.IsExistsPublisherById
{
    public class IsExistsPublisherByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public IsExistsPublisherByIdQuery(long id)
        {
            Id = id;
        }
    }
}
