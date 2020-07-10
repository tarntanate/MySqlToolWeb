﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, HttpResult<UserDto>>
    {
        private AdsDbRepository<UserEntity> userDbRepo { get; }

        public GetUserByIdQueryHandler(AdsDbRepository<UserEntity> authUserDbRepo)
        {
            userDbRepo = authUserDbRepo;
        }

        public async Task<HttpResult<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<UserDto>> GetOnDb(GetUserByIdQuery request)
        {
            var result = new HttpResult<UserDto>();

            var item = await userDbRepo.FirstAsync(
                selector: UserDto.Projection,
                filter: f =>
                    f.Id == request.Id
            );

            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"User '{request.Id}' doesn't exist.");
        }
    }
}