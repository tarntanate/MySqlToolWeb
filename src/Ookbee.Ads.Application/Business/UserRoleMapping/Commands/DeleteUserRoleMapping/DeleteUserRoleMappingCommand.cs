using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.DeleteUserRoleMapping
{
    public class DeleteUserRoleMappingCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteUserRoleMappingCommand(long id)
        {
            Id = id;
        }
    }
}
