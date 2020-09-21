using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Queries.GetUserPermissionByName
{
    public class GetUserPermissionByNameQuery : IRequest<HttpResult<UserPermissionDto>>
    {
        public string ExtensionName { get; set; }

        public GetUserPermissionByNameQuery(string name)
        {
            ExtensionName = name;
        }
    }
}
