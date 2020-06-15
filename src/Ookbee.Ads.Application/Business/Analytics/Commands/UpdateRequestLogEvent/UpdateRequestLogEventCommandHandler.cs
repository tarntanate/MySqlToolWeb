using System;
using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Analytics.Queries.GetRequestLogById;
using Ookbee.Ads.Application.Infrastructure.Enums;
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
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> CreateOnDb(UpdateRequestLogEventCommand request)
        {
            var result = new HttpResult<bool>();

            var requestLogResult = await Mediator.Send(new GetRequestLogByIdQuery(request.EventId));
            if (!requestLogResult.Ok)
                return result.Fail(requestLogResult.StatusCode, requestLogResult.Message);
            
            if (requestLogResult.Data.IsFill)
            {
                switch (request.EventType.ToUpper())
                {
                    case "CLICK":
                        requestLogResult.Data.IsClick = true;
                        break;

                    case "DISPLAY":
                        requestLogResult.Data.IsDisplay = true;
                        break;

                    case "IMPRESSION":
                        requestLogResult.Data.IsImpression = true;
                        break;

                    default:
                        throw new InvalidOperationException();
                }

                var entity = Mapper
                    .Map(requestLogResult.Data)
                    .ToANew<RequestLogEntity>();

                await RequestLogDbRepo.UpdateAsync(entity.Id, entity);
                await RequestLogDbRepo.SaveChangesAsync();
            }

            return result.Success(true);
        }
    }
}
