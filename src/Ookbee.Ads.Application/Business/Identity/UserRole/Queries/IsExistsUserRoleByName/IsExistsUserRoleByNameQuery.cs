using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Identity.UserRole.Queries.IsExistsUserRoleByName
{
    public class IsExistsUserRoleByNameQuery : IRequest<Response<bool>>
    {
        public string Name { get; set; }

        public IsExistsUserRoleByNameQuery(string name)
        {
            Name = name;
        }
    }
}
