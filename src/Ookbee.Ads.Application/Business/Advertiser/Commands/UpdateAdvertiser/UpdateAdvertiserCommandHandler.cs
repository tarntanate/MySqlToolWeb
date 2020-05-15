using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.UpdateAdvertiser
{
    public class UpdateAdvertiserCommandHandler : IRequestHandler<UpdateAdvertiserCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private OokbeeAdsMongoDBRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public UpdateAdvertiserCommandHandler(
            IMediator mediator,
            OokbeeAdsMongoDBRepository<AdvertiserDocument> advertiserMongoDB)
        {
            Mediator = mediator;
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<AdvertiserDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(AdvertiserDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsResult = await Mediator.Send(new IsExistsAdvertiserByIdQuery(document.Id));
                if (!isExistsResult.Ok)
                    return isExistsResult;

                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await AdvertiserMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
