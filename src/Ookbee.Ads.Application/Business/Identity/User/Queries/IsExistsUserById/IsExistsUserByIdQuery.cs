using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Identity.User.Queries.IsExistsUserById
{
    public class IsExistsUserByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsUserByIdQuery(long id)
        {
            Id = id;
        }
    }
}
