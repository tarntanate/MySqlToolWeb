using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.RequestLogEntities;
using Ookbee.Ads.Persistence.EFCore.TimeScaleDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateGroupRequestLog
{
    public class CreateGroupRequestLogCommandHandler : IRequestHandler<CreateGroupRequestLogCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private TimeScaleDbRepository<GroupRequestLogEntity> TimeScaleDbRepo { get; }

        public CreateGroupRequestLogCommandHandler(
            IMapper mapper,
            TimeScaleDbRepository<GroupRequestLogEntity> timeScaleDbRepo)
        {
            Mapper = mapper;
            TimeScaleDbRepo = timeScaleDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(CreateGroupRequestLogCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<GroupRequestLogEntity>(request);
            await TimeScaleDbRepo.InsertAsync(entity);
            await TimeScaleDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<bool>();
            if (result.Ok)
                return result.Success(true);

            return result.Fail(result.StatusCode);
        }
    }
}
