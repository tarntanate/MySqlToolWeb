using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.RequestLog.Queries.IsExistsRequestLogById
{
    public class IsExistsRequestLogByIdQueryHandler : IRequestHandler<IsExistsRequestLogByIdQuery, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public IsExistsRequestLogByIdQueryHandler(
            IMediator mediator,
            AdsMongoRepository<AdDocument> adMongoDB)
        {
            Mediator = mediator;
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsRequestLogByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(IsExistsRequestLogByIdQuery request)
        {
            var result = new HttpResult<bool>();
            var isExists = await AdMongoDB.AnyAsync(
                filter: f => f.Id == request.Id &&
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"RequestLog '{request.Id}' doesn't exist.");
        }
    }
}
