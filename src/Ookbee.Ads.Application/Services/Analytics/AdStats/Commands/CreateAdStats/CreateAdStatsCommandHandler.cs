﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.CreateAdStats
{
    public class CreateAdStatsCommandHandler : IRequestHandler<CreateAdStatsCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public CreateAdStatsCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            Mapper = mapper;
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdStatsCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdStatsEntity>(request);
            await AdStatsDbRepo.InsertAsync(entity);
            await AdStatsDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<long>();
            return result.OK(entity.Id);
        }
    }
}
