using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Identity.UserPermission;
using Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.CreateUserPermission;
using Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.DeleteUserPermission;
using Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.UpdateUserPermission;
using Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.GetUserPermissionById;
using Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.GetUserPermissionList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/user-role-permissions")]
    public class UserRolePermissionsController : ApiController
    {
        [HttpGet]
        public async Task<Response<IEnumerable<UserPermissionDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? roleId, CancellationToken cancellationToken)
            => await Mediator.Send(new GetUserPermissionListQuery(start, length, roleId), cancellationToken);
        
        [HttpGet("{id}")]
        public async Task<Response<UserPermissionDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetUserPermissionByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<Response<long>> Create([FromBody] CreateUserPermissionRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateUserPermissionCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<Response<bool>> Update([FromRoute] long id, [FromBody] UpdateUserPermissionRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateUserPermissionCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteUserPermissionCommand(id), cancellationToken);
    }
}
