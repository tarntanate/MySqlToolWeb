using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Identity.UserRole.Queries.GetUserRoleByName
{
    public class GetUserRoleByNameQuery : IRequest<Response<UserRoleDto>>
    {
        public string Name { get; set; }

        public GetUserRoleByNameQuery(string name)
        {
            Name = name;
        }
    }
}
