using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupById
{
    public class IsExistsAdGroupByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public bool? Enabled { get; private set; }

        public IsExistsAdGroupByIdQuery(long id, bool? enabled)
        {
            Id = id;
            Enabled = enabled;
        }
    }
}
