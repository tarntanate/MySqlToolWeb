using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.CreateAdUnitStats
{
    public class CreateAdUnitStatsCommandHandler : IRequestHandler<CreateAdUnitStatsCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdUnitStatsEntity> RequestLogDbRepo;

        public CreateAdUnitStatsCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdUnitStatsEntity> requestLogDbRepo)
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
