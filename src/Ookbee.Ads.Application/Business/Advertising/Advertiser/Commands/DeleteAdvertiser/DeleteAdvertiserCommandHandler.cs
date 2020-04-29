using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Advertising.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommandHandler : IRequestHandler<DeleteAdvertiserCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoRepository<AdvertiserDocument> AdvertiserMongoRepo { get; }

        public DeleteAdvertiserCommandHandler(OokbeeAdsMongoRepository<AdvertiserDocument> advertiserMongoRepo)
        {
            AdvertiserMongoRepo = advertiserMongoRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnMongo(DeleteAdvertiserCommand request)
        {
            var result = new HttpResult<bool>();
            await AdvertiserMongoRepo.DeleteAsync(request.Id);
            return result.Success(true);
        }
    }
}
