using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRole.Queries.IsExistsUserRoleByName
{
    public class IsExistsUserRoleByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsUserRoleByNameQuery(string name)
        {
            Name = name;
        }
    }
}
