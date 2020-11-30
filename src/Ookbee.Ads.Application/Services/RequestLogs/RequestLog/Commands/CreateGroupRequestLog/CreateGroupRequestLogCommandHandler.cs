using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.RequestLogEntities;
using Ookbee.Ads.Persistence.EFCore.TimeScaleDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateGroupRequestLog
{
    public class CreateGroupRequestLogCommandHandler : IRequestHandler<CreateGroupRequestLogCommand, Response<bool>>
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

        public async Task<Response<bool>> Handle(CreateGroupRequestLogCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<GroupRequestLogEntity>(request);
            await TimeScaleDbRepo.InsertAsync(entity);
            await TimeScaleDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            if (result.IsSuccess)
                return result.OK(true);

            return result.NotFound();
        }
    }
}
