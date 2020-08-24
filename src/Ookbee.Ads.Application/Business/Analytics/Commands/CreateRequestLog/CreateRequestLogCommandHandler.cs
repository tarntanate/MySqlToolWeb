using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.Commands.CreateRequestLog
{
    public class CreateRequestLogCommandHandler : IRequestHandler<CreateRequestLogCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AnalyticsDbRepository<RequestLogEntity> RequestLogDbRepo { get; }

        public CreateRequestLogCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AnalyticsDbRepository<RequestLogEntity> requestLogDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            RequestLogDbRepo = requestLogDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateRequestLogCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<RequestLogEntity>(request);
            await RequestLogDbRepo.InsertAsync(entity);
            await RequestLogDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<long>();
            return result.Success(entity.Id);
        }
    }
}
