using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.ActivityLog.Queries.GetActivityLogList
{
    public class GetActivityLogListQuery : IRequest<HttpResult<IEnumerable<ActivityLogDto>>>
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public long? UserId { get; set; }

        public string ObjectName { get; set; }

        public GetActivityLogListQuery(int start, int length, long? userId, string objectName)
        {
            Start = start;
            Length = length;
            UserId = userId;
            ObjectName = objectName;
        }
    }
}
