using AgileObjects.AgileMapper;
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
        private IMediator Mediator { get; }
        private AnalyticsDbRepository<RequestLogEntity> RequestLogDbRepo { get; }

        public CreateRequestLogCommandHandler(
            IMediator mediator,
            AnalyticsDbRepository<RequestLogEntity> requestLogDbRepo)
        {
            Mediator = mediator;
            RequestLogDbRepo = requestLogDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateRequestLogCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateRequestLogCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper
                .Map(request)
                .ToANew<RequestLogEntity>();

            await RequestLogDbRepo.InsertAsync(entity);
            await RequestLogDbRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
