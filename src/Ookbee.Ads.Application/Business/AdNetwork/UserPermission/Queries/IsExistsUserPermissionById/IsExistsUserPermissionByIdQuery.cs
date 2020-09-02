using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Queries.IsExistsUserPermissionById
{
    public class IsExistsUserPermissionByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsUserPermissionByIdQuery(long id)
        {
            Id = id;
        }
    }
}
