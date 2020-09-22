﻿using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.UserRoleMapping.Commands.UpdateUserRoleMapping
{
    public class UpdateUserRoleMappingCommandHandler : IRequestHandler<UpdateUserRoleMappingCommand, Response<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<UserRoleMappingEntity> UserRoleMappingDbRepo { get; }

        public UpdateUserRoleMappingCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<UserRoleMappingEntity> userRoleMappingDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            UserRoleMappingDbRepo = userRoleMappingDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateUserRoleMappingCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserRoleMappingEntity>(request);
            await UserRoleMappingDbRepo.UpdateAsync(new { request.UserId, request.RoleId }, entity);
            await UserRoleMappingDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
