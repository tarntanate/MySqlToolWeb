using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.GetUserRoleByName
{
    public class GetUserRoleByNameQuery : IRequest<Response<UserRoleDto>>
    {
        public string Name { get; private set; }

        public GetUserRoleByNameQuery(string name)
        {
            Name = name;
        }
    }
}
