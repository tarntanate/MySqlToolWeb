using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Queries.IsExistsUserPermissionByName
{
    public class IsExistsUserPermissionByNameQuery : IRequest<HttpResult<bool>>
    {
        public string ExtensionName { get; set; }

        public IsExistsUserPermissionByNameQuery(string name)
        {
            ExtensionName = name;
        }
    }
}
