using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Identity.UserPermission.Queries.GetUserPermissionById
{
    public class GetUserPermissionByIdQuery : IRequest<Response<UserPermissionDto>>
    {
        public long Id { get; set; }

        public GetUserPermissionByIdQuery(long id)
        {
            Id = id;
        }
    }
}
