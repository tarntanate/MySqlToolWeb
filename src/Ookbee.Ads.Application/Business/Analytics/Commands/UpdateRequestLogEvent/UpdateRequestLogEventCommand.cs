using MediatR;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.Commands.UpdateRequestLogEvent
{
    public class UpdateRequestLogEventCommand : IRequest<HttpResult<bool>>
    {
        public long EventId { get; set; }
        public string EventType { get; set; }

        public UpdateRequestLogEventCommand(long eventId, string eventType)
        {
            EventId = eventId;
            EventType = eventType;
        }
    }
}