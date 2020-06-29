using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UserRole.Queries.IsExistsUserRoleById
{
    public class IsExistsUserRoleByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsUserRoleByIdQuery(long id)
        {
            Id = id;
        }
    }
}
