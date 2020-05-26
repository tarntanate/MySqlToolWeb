using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.RequestLog.Queries.IsExistsRequestLogById
{
    public class IsExistsRequestLogByIdQuery : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsRequestLogByIdQuery(string id)
        {
            Id = id;
        }
    }
}
