using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserById;
using Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserByName;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserByName;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.UpdateAdvertiser
{
    public class UpdateAdvertiserCommandHandler : IRequestHandler<UpdateAdvertiserCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public UpdateAdvertiserCommandHandler(
            IMediator mediator,
            AdsMongoRepository<AdvertiserDocument> advertiserMongoDB)
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
                var advertiserByIdResult = await Mediator.Send(new GetAdvertiserByIdQuery(document.Id));
                if (!advertiserByIdResult.Ok)
                    return result.Fail(advertiserByIdResult.StatusCode, advertiserByIdResult.Message);
                    
                var advertiserResult = await Mediator.Send(new GetAdvertiserByNameQuery(document.Name));
                if (advertiserResult.Ok && 
                    advertiserResult.Data.Id != document.Id && 
                    advertiserResult.Data.Name == document.Name)
                    return result.Fail(409, $"Advertiser '{document.Name}' already exists.");

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
