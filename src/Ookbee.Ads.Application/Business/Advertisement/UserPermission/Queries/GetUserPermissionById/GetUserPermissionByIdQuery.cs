using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Queries.GetUserPermissionById
{
    public class GetUserPermissionByIdQuery : IRequest<HttpResult<UserPermissionDto>>
    {
        public long Id { get; set; }

        public GetUserPermissionByIdQuery(long id)
        {
            Id = id;
        }
    }
}
