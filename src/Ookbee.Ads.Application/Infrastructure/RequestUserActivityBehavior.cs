using MediatR;
using Ookbee.Ads.Application.Business.ActivityLog.Commands.CreateActivityLog;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Enums;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure
{
    public class RequestUserActivityBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private IMediator Mediator { get; }

        public RequestUserActivityBehavior(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();
            var requestName = request.GetType().Name;
            var activities = Enum.GetValues(typeof(LogEvent));
            foreach (var activity in activities)
            {
                if (requestName.Contains(activity.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    if (typeof(HttpResult<>).IsAssignableFrom(response.GetType().GetGenericTypeDefinition()))
                    {
                        var data = response as dynamic;
                        var dataLogger = data.DataLogger as DataLogger;
                        if (dataLogger != null)
                        {
                            var objectId = dataLogger.ObjectId;
                            var objectData = dataLogger.ObjectData;
                            var createActivityLogCommand = new CreateActivityLogCommand(objectId, objectData, (LogEvent)activity);
                            await Mediator.Send(createActivityLogCommand);
                        }
                    }
                    break;
                }
            }

            return response;
        }
    }
}
