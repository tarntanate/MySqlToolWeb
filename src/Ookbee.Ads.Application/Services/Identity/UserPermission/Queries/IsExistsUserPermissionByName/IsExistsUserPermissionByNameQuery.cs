using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.IsExistsUserPermissionByName
{
    public class IsExistsUserPermissionByNameQuery : IRequest<Response<bool>>
    {
        public string ExtensionName { get; set; }

        public IsExistsUserPermissionByNameQuery(string name)
        {
            ExtensionName = name;
        }
    }
}
