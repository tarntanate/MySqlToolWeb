using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.RequestLogEntities;
using Ookbee.Ads.Persistence.EFCore.TimeScaleDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.RequestLogs.AdClickLog.Commands.CreateAdClickLog
{
    public class CreateAdClickLogCommandHandler : IRequestHandler<CreateAdClickLogCommand, Response<bool>>
    {
        private IMapper Mapper { get; }
        private TimeScaleDbRepository<AdClickLogEntity> TimeScaleDbRepo { get; }

        public CreateAdClickLogCommandHandler(
            IMapper mapper,
            TimeScaleDbRepository<AdClickLogEntity> timeScaleDbRepo)
        {
            Mapper = mapper;
            TimeScaleDbRepo = timeScaleDbRepo;
        }

        public async Task<Response<bool>> Handle(CreateAdClickLogCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdClickLogEntity>(request);
            await TimeScaleDbRepo.InsertAsync(entity);
            await TimeScaleDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            if (result.Ok)
                return result.Success(true);

            return result.Fail(result.StatusCode);
        }
    }
}
