using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using Ookbee.Ads.Persistence.EFCore.TimeScaleDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateRequestLog
{
    public class CreateRequestLogCommandHandler : IRequestHandler<CreateRequestLogCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private TimeScaleDbRepository<RequestLogEntity> TimeScaleDbRepo { get; }

        public CreateRequestLogCommandHandler(
            IMapper mapper,
            TimeScaleDbRepository<RequestLogEntity> timeScaleDbRepo)
        {
            Mapper = mapper;
            TimeScaleDbRepo = timeScaleDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(CreateRequestLogCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<RequestLogEntity>(request);
            await TimeScaleDbRepo.InsertAsync(entity);
            await TimeScaleDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<bool>();
            if (result.Ok)
                return result.Success(true);

            return result.Fail(result.StatusCode);
        }
    }
}
