using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.GetUserPermissionById
{
    public class GetUserPermissionByIdQuery : IRequest<Response<UserPermissionDto>>
    {
        public long Id { get; private set; }

        public GetUserPermissionByIdQuery(long id)
        {
            Id = id;
        }
    }
}
