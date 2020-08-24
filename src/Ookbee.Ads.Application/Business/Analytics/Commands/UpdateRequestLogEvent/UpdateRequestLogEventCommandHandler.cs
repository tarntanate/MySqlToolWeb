using System;
using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Analytics.Queries.GetRequestLogById;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.Commands.UpdateRequestLogEvent
{
    public class UpdateRequestLogEventCommandHandler : IRequestHandler<UpdateRequestLogEventCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AnalyticsDbRepository<RequestLogEntity> RequestLogDbRepo { get; }

        public UpdateRequestLogEventCommandHandler(
            IMediator mediator,
            AnalyticsDbRepository<RequestLogEntity> requestLogDbRepo)
        {
            Mediator = mediator;
            RequestLogDbRepo = requestLogDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateRequestLogEventCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var requestLogResult = await Mediator.Send(new GetRequestLogByIdQuery(request.EventId), cancellationToken);
            if (!requestLogResult.Ok)
                return result.Fail(requestLogResult.StatusCode, requestLogResult.Message);

            if (requestLogResult.Data.IsFill &&
                Enum.TryParse<AdEvent>(request.EventType, true, out var adsEvent))
            {
                switch (adsEvent)
                {
                    case AdEvent.Click:
                        requestLogResult.Data.IsClick = true;
                        break;

                    case AdEvent.Display:
                        requestLogResult.Data.IsDisplay = true;
                        break;

                    case AdEvent.Impression:
                        requestLogResult.Data.IsImpression = true;
                        break;

                    default:
                        throw new InvalidOperationException();
                }

                var entity = Mapper
                    .Map(requestLogResult.Data)
                    .ToANew<RequestLogEntity>();

                await RequestLogDbRepo.UpdateAsync(entity.Id, entity);
                await RequestLogDbRepo.SaveChangesAsync(cancellationToken);
            }

            return result.Success(true);
        }
    }
}
