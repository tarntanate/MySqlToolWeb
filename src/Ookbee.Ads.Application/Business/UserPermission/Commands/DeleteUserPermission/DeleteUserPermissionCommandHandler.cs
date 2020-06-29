﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UserPermission.Commands.DeleteUserPermission
{
    public class DeleteUserPermissionCommandHandler : IRequestHandler<DeleteUserPermissionCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<UserPermissionEntity> UserPermissionDbRepo { get; }

        public DeleteUserPermissionCommandHandler(
            IMediator mediator,
            AdsDbRepository<UserPermissionEntity> userPermissionDbRepo)
        {
            Mediator = mediator;
            UserPermissionDbRepo = userPermissionDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteUserPermissionCommand request)
        {
            var result = new HttpResult<bool>();

            await UserPermissionDbRepo.DeleteAsync(request.Id);
            await UserPermissionDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
