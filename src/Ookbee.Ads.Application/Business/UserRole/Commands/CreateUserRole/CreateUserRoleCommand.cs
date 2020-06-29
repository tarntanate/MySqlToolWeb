﻿using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UserRole.Commands.CreateUserRole
{
    public class CreateUserRoleCommand : CreateUserRoleRequest, IRequest<HttpResult<long>>
    {
        public CreateUserRoleCommand(CreateUserRoleRequest request)
        {
            Name = request.Name;
            Description = request.Description;
        }
    }
}
