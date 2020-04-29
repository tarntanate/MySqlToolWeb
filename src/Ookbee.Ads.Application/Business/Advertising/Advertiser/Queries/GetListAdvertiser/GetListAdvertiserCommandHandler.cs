using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Advertising.Advertiser.ViewModels;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Advertising.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.GetListAdvertiser
{
    public class GetListAdvertiserCommandHandler : IRequestHandler<GetListAdvertiserCommand, HttpResult<IEnumerable<AdvertiserViewModel>>>
    {
        private OokbeeAdsMongoRepository<AdvertiserDocument> AdvertiserMongoRepo { get; }

        public GetListAdvertiserCommandHandler(OokbeeAdsMongoRepository<AdvertiserDocument> advertiserMongoRepo)
        {
            AdvertiserMongoRepo = advertiserMongoRepo;
        }

        public async Task<HttpResult<IEnumerable<AdvertiserViewModel>>> Handle(GetListAdvertiserCommand request, CancellationToken cancellationToken)
        {
            return await FindOnMongo(request);
        }

        private async Task<HttpResult<IEnumerable<AdvertiserViewModel>>> FindOnMongo(GetListAdvertiserCommand request)
        {
            var result = new HttpResult<IEnumerable<AdvertiserViewModel>>();
            var items = await AdvertiserMongoRepo.FindAsync(
                sort: Builders<AdvertiserDocument>.Sort.Descending(nameof(AdvertiserDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<AdvertiserViewModel>>();
            var y = data.ToList();
            return result.Success(data);
        }
    }
}
