using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.IsExistsUserPermissionById
{
    public class IsExistsUserPermissionByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsUserPermissionByIdQuery(long id)
        {
            Id = id;
        }
    }
}
