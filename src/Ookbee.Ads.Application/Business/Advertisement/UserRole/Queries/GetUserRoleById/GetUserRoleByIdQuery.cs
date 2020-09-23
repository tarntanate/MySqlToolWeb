using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRole.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQuery : IRequest<Response<UserRoleDto>>
    {
        public long Id { get; set; }

        public GetUserRoleByIdQuery(long id)
        {
            Id = id;
        }
    }
}
