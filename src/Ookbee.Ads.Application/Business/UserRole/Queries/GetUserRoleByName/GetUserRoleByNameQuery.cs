﻿using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UserRole.Queries.GetUserRoleByName
{
    public class GetUserRoleByNameQuery : IRequest<HttpResult<UserRoleDto>>
    {
        public string Name { get; set; }

        public GetUserRoleByNameQuery(string name)
        {
            Name = name;
        }
    }
}