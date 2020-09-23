using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupById
{
    public class IsExistsAdGroupByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdGroupByIdQuery(long id)
        {
            Id = id;
        }
    }
}
