﻿using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.User;
using Ookbee.Ads.Application.Business.User.Commands.CreateUser;
using Ookbee.Ads.Application.Business.User.Commands.UpdateUser;
using Ookbee.Ads.Application.Business.User.Queries.GetUserById;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<HttpResult<UserDto>> GetById([FromRoute] long id, CancellationToken cancellationToken) => await Mediator.Send(new GetUserByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken) => await Mediator.Send(new CreateUserCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] long id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken) => await Mediator.Send(new UpdateUserCommand(id, request), cancellationToken);
    }
}