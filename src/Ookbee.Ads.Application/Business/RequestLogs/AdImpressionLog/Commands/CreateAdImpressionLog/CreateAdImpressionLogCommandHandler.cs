using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.RequestLogEntities;
using Ookbee.Ads.Persistence.EFCore.TimeScaleDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.RequestLogs.AdImpressionLog.Commands.CreateAdImpressionLog
{
    public class CreateAdImpressionLogCommandHandler : IRequestHandler<CreateAdImpressionLogCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private TimeScaleDbRepository<AdImpressionLogEntity> TimeScaleDbRepo { get; }

        public CreateAdImpressionLogCommandHandler(
            IMapper mapper,
            TimeScaleDbRepository<AdImpressionLogEntity> timeScaleDbRepo)
        {
            Mapper = mapper;
            TimeScaleDbRepo = timeScaleDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(CreateAdImpressionLogCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdImpressionLogEntity>(request);
            await TimeScaleDbRepo.InsertAsync(entity);
            await TimeScaleDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<bool>();
            if (result.Ok)
                return result.Success(true);

            return result.Fail(result.StatusCode);
        }
    }
}
