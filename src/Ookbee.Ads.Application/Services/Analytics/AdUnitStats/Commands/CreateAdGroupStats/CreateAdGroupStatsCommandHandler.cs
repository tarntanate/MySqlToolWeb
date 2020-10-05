﻿using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.CreateAdUnitStats
{
    public class CreateAdUnitStatsCommandHandler : IRequestHandler<CreateAdUnitStatsCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AnalyticsDbRepository<AdUnitStatsEntity> RequestLogDbRepo;

        public CreateAdUnitStatsCommandHandler(
            IMapper mapper,
            AnalyticsDbRepository<AdUnitStatsEntity> requestLogDbRepo)
        {
            Mapper = mapper;
            RequestLogDbRepo = requestLogDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdUnitStatsCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdUnitStatsEntity>(request);
            await RequestLogDbRepo.InsertAsync(entity);
            await RequestLogDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<long>();
            return result.OK(entity.Id);
        }
    }
}