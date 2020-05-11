using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Commands.UpdateBanner
{
    public class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<BannerDocument> BannerMongoDB { get; }

        public UpdateBannerCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<BannerDocument> bannerMongoDB)
        {
            Mediatr = mediatr;
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
