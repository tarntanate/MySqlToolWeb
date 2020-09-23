using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Identity.User;
using Ookbee.Ads.Application.Business.Identity.User.Commands.CreateUser;
using Ookbee.Ads.Application.Business.Identity.User.Commands.UpdateUser;
using Ookbee.Ads.Application.Business.Identity.User.Queries.GetUserById;
using Ookbee.Ads.Application.Business.Identity.User.Queries.GetUserList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ApiController
    {
        [HttpGet]
        public async Task<Response<IEnumerable<UserDto>>> GetList([FromQuery] int start, [FromQuery] int length, CancellationToken cancellationToken)
           => await Mediator.Send(new GetUserListQuery(start, length), cancellationToken);

        [HttpGet("{id}")]
        public async Task<Response<UserDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetUserByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<Response<long>> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateUserCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<Response<bool>> Update([FromRoute] long id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateUserCommand(id, request), cancellationToken);
    }
}
