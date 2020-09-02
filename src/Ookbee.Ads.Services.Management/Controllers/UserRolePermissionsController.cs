using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdNetwork.UserPermission;
using Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Commands.CreateUserPermission;
using Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Commands.DeleteUserPermission;
using Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Commands.UpdateUserPermission;
using Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Queries.GetUserPermissionById;
using Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Queries.GetUserPermissionList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
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
        public async Task<HttpResult<IEnumerable<UserPermissionDto>>> GetList([FromQuery] int start, [FromQuery] int length, CancellationToken cancellationToken)
            => await Mediator.Send(new GetUserPermissionListQuery(start, length), cancellationToken);

        [HttpGet("{id}")]
        public async Task<HttpResult<UserPermissionDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetUserPermissionByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody] CreateUserPermissionRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateUserPermissionCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] long id, [FromBody] UpdateUserPermissionRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateUserPermissionCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteUserPermissionCommand(id), cancellationToken);
    }
}
