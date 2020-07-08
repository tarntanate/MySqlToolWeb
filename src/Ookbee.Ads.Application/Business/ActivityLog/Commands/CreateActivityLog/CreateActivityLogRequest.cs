using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.ActivityLog.Commands.CreateActivityLog
{
    public class CreateActivityLogRequest
    {
        public long UserId { get; set; }
        public long ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string ObjectData { get; set; }
        public LogEvent Activity { get; set; }
    }
}
