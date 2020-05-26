using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserByName;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Common;
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
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdateAdvertiserCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsByIdResult = await Mediator.Send(new IsExistsAdvertiserByIdQuery(request.Id));
                if (!isExistsByIdResult.Ok)
                    return isExistsByIdResult;

                var advertiserResult = await Mediator.Send(new GetAdvertiserByNameQuery(request.Name));
                if (advertiserResult.Ok &&
                    advertiserResult.Data.Id != request.Id &&
                    advertiserResult.Data.Name == request.Name)
                    return result.Fail(409, $"Advertiser '{request.Name}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<AdvertiserDocument>();
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
