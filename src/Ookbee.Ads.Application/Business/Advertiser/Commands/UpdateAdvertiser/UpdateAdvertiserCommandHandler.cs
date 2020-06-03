using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserByName;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserById;

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
                var advertiserResult = await Mediator.Send(new GetAdvertiserByIdQuery(request.Id));
                if (!advertiserResult.Ok)
                    return result.Fail(advertiserResult.StatusCode, advertiserResult.Message);

                var advertiserByNameResult = await Mediator.Send(new GetAdvertiserByNameQuery(request.Name));
                if (advertiserByNameResult.Ok &&
                    advertiserByNameResult.Data.Id != request.Id &&
                    advertiserByNameResult.Data.Name == request.Name)
                    return result.Fail(409, $"Advertiser '{request.Name}' already exists.");

                var template = Mapper.Map(request).Over(advertiserResult.Data);
                var document = Mapper.Map(template).ToANew<AdvertiserDocument>();
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
