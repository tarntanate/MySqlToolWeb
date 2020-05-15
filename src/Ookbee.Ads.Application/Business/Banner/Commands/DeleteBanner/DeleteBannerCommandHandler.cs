using MediatR;
using Ookbee.Ads.Application.Business.Banner.Queries.IsExistsBannerById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Commands.DeleteBanner
{
    public class DeleteBannerCommandHandler : IRequestHandler<DeleteBannerCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoDBRepository<BannerDocument> BannerMongoDB { get; }

        public DeleteBannerCommandHandler(
            IMediator mediator,
            AdsMongoDBRepository<BannerDocument> bannerMongoDB)
        {
            Mediator = mediator;
            BannerMongoDB = bannerMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsBannerByIdQuery(id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await BannerMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
