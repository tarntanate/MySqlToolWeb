using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Identity.UserRoleMapping;
using Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Commands.CreateUserRoleMapping;
using Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Commands.DeleteUserRoleMapping;
using Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Commands.UpdateUserRoleMapping;
using Ookbee.Ads.Application.Services.Identity.UserRoleMapping.Queries.GetUserRoleMappingList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/user-role-mappings")]
    public class UserRoleMappinsController : ApiController
    {
        [HttpGet]
        public async Task<Response<IEnumerable<UserRoleMappingDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? userId, [FromQuery] long? roleId, CancellationToken cancellationToken)
            => await Mediator.Send(new GetUserRoleMappingListQuery(start, length, userId, roleId), cancellationToken);

        [HttpPost]
        public async Task<Response<bool>> Create([FromBody] CreateUserRoleMappingRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateUserRoleMappingCommand(request), cancellationToken);

        [HttpPut]
        public async Task<Response<bool>> Update([FromBody] UpdateUserRoleMappingRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateUserRoleMappingCommand(request), cancellationToken);

        [HttpDelete]
        public async Task<Response<bool>> Delete([FromRoute] long userId, [FromRoute] long roleId, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteUserRoleMappingCommand(userId, roleId), cancellationToken);
    }
}
