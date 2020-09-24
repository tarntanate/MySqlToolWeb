using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.IsExistsUserRoleByName
{
    public class IsExistsUserRoleByNameQuery : IRequest<Response<bool>>
    {
        public string Name { get; private set; }

        public IsExistsUserRoleByNameQuery(string name)
        {
            Name = name;
        }
    }
}
