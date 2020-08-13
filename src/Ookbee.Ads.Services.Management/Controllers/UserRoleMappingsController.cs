using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.UserRoleMapping;
using Ookbee.Ads.Application.Business.UserRoleMapping.Commands.CreateUserRoleMapping;
using Ookbee.Ads.Application.Business.UserRoleMapping.Commands.DeleteUserRoleMapping;
using Ookbee.Ads.Application.Business.UserRoleMapping.Commands.UpdateUserRoleMapping;
using Ookbee.Ads.Application.Business.UserRoleMapping.Queries.GetUserRoleMappingById;
using Ookbee.Ads.Application.Business.UserRoleMapping.Queries.GetUserRoleMappingList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/user-role-mappings")]
    public class UserRoleMappingMappingsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<UserRoleMappingDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? userId, [FromQuery] long? roleId, CancellationToken cancellationToken)
            => await Mediator.Send(new GetUserRoleMappingListQuery(start, length, userId, roleId), cancellationToken);

        // [HttpGet]
        // public async Task<HttpResult<UserRoleMappingDto>> GetById([FromRoute] long userId, [FromRoute] long roleId, CancellationToken cancellationToken)
        //     => await Mediator.Send(new GetUserRoleMappingByIdQuery(userId, roleId), cancellationToken);

        [HttpPost]
        public async Task<HttpResult<bool>> Create([FromBody] CreateUserRoleMappingRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateUserRoleMappingCommand(request), cancellationToken);

        [HttpPut]
        public async Task<HttpResult<bool>> Update([FromBody] UpdateUserRoleMappingRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateUserRoleMappingCommand(request), cancellationToken);

        [HttpDelete]
        public async Task<HttpResult<bool>> Delete([FromRoute] long userId, [FromRoute] long roleId, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteUserRoleMappingCommand(userId, roleId), cancellationToken);
    }
}
