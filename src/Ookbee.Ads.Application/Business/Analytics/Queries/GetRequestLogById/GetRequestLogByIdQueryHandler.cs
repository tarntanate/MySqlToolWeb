using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.Queries.GetRequestLogById
{
    public class GetRequestLogByIdQueryHandler : IRequestHandler<GetRequestLogByIdQuery, HttpResult<RequestLogDto>>
    {
        private AnalyticsDbRepository<RequestLogEntity> RequestLogDbRepo { get; }

        public GetRequestLogByIdQueryHandler(AnalyticsDbRepository<RequestLogEntity> adUnitTypeDbRepo)
        {
            RequestLogDbRepo = adUnitTypeDbRepo;
        }

        public async Task<HttpResult<RequestLogDto>> Handle(GetRequestLogByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<RequestLogDto>> GetOnDb(GetRequestLogByIdQuery request)
        {
            var result = new HttpResult<RequestLogDto>();

            var item = await RequestLogDbRepo.FirstAsync(filter: f => f.Id == request.Id);
            if (item == null)
                return result.Fail(404, $"RequestLog '{request.Id}' doesn't exist.");
                
            var data = Mapper
                .Map(item)
                .ToANew<RequestLogDto>();

            return result.Success(data);
        }
    }
}
