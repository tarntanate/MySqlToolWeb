using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsByIdAdvertiser;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.UpdateAdvertiser
{
    public class UpdateAdvertiserCommandHandler : IRequestHandler<UpdateAdvertiserCommand, HttpResult<string>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public UpdateAdvertiserCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<AdvertiserDocument> advertiserMongoDB)
        {
            Mediatr = mediatr;
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<string>> Handle(UpdateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<AdvertiserDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<string>> UpdateOnMongo(AdvertiserDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var isExistsByNameResult = await Mediatr.Send(new IsExistsByIdAdvertiserCommand(document.Id));
                if (!isExistsByNameResult.Data)
                    return result.Fail(404, $"Advertiser doesn't exist.");

                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await AdvertiserMongoDB.UpdateAsync(document.Id, document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
