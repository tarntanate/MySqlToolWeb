using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.ActivityLog.Commands.CreateActivityLog
{
    public class CreateActivityLogCommand : CreateActivityLogRequest, IRequest<HttpResult<long>>
    {
        public CreateActivityLogCommand(long id, object data, LogEvent activity)
        {
            UserId = 6383511;
            ObjectId = id;
            ObjectType = data.GetType().Name;
            ObjectData = JsonHelper.Serialize(data);
            Activity = activity;
        }
    }
}
