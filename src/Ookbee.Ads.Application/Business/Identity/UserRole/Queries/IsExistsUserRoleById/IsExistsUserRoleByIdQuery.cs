using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Identity.UserRole.Queries.IsExistsUserRoleById
{
    public class IsExistsUserRoleByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsUserRoleByIdQuery(long id)
        {
            Id = id;
        }
    }
}
