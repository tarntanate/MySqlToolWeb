using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.RequestLogs.Application.Business.RequestLog.Commands.DeleteRequestLog
{
    public class DeleteRequestLogCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteRequestLogCommand(string id)
        {
            Id = id;
        }
    }
}
