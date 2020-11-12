using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.UpdateAdUnitStats
{
    public class UpdateAdUnitStatsCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }
        public long Id { get; private set; }
        public long AdUnitId { get; private set; }
        public long Request { get; private set; }
        public long Fill { get; private set; }

        public UpdateAdUnitStatsCommand(DateTimeOffset caculatedAt, long id, long adUnitId, long request, long fill)
        {
            Id = id;
            AdUnitId = adUnitId;
            Request = request;
            Fill = fill;
            CaculatedAt = caculatedAt;
        }
    }
}
