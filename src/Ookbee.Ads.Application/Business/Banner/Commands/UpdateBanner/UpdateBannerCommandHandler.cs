using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Banner.Queries.IsExistsBannerById;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Commands.UpdateBanner
{
    public class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<BannerDocument> BannerMongoDB { get; }

        public UpdateBannerCommandHandler(
            IMediator mediator,
            AdsMongoRepository<BannerDocument> bannerMongoDB)
        {
            Mediator = mediator;
            BannerMongoDB = bannerMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<BannerDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(BannerDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsResult = await Mediator.Send(new IsExistsBannerByIdQuery(document.Id));
                if (!isExistsResult.Ok)
                    return isExistsResult;

                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await BannerMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
