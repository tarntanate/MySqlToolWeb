using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Advertisement.UserRole;
using Ookbee.Ads.Application.Business.Advertisement.UserRole.Commands.CreateUserRole;
using Ookbee.Ads.Application.Business.Advertisement.UserRole.Commands.DeleteUserRole;
using Ookbee.Ads.Application.Business.Advertisement.UserRole.Commands.UpdateUserRole;
using Ookbee.Ads.Application.Business.Advertisement.UserRole.Queries.GetUserRoleById;
using Ookbee.Ads.Application.Business.Advertisement.UserRole.Queries.GetUserRoleList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/user-roles")]
    public class UserRolesController : ApiController
    {
        [HttpGet]
        public async Task<Response<IEnumerable<UserRoleDto>>> GetList([FromQuery] int start, [FromQuery] int length, CancellationToken cancellationToken)
            => await Mediator.Send(new GetUserRoleListQuery(start, length), cancellationToken);

        [HttpGet("{id}")]
        public async Task<Response<UserRoleDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetUserRoleByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<Response<long>> Create([FromBody] CreateUserRoleRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateUserRoleCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<Response<bool>> Update([FromRoute] long id, [FromBody] UpdateUserRoleRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateUserRoleCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteUserRoleCommand(id), cancellationToken);
    }
}
