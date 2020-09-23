using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Identity.UserPermission.Queries.GetUserPermissionByName
{
    public class GetUserPermissionByNameQuery : IRequest<Response<UserPermissionDto>>
    {
        public string ExtensionName { get; set; }

        public GetUserPermissionByNameQuery(string name)
        {
            ExtensionName = name;
        }
    }
}
