using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.Queries.GetRequestLogById
{
    public class GetRequestLogByIdQuery : IRequest<HttpResult<RequestLogDto>>
    {
        public long Id { get; set; }

        public GetRequestLogByIdQuery(long id)
        {
            Id = id;
        }
    }
}
