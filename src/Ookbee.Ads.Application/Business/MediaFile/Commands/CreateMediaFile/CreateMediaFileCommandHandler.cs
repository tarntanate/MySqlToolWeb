using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Banner.Queries.GetBannerById;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.CreateMediaFile
{
    public class CreateMediaFileCommandHandler : IRequestHandler<CreateMediaFileCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private OokbeeAdsMongoDBRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public CreateMediaFileCommandHandler(
            IMediator mediator,
            OokbeeAdsMongoDBRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateMediaFileCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<MediaFileDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(MediaFileDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var bannerResult = await Mediator.Send(new GetBannerByIdQuery(document.BannerId));
                if (!bannerResult.Ok)
                    return result.Fail(bannerResult.StatusCode, bannerResult.Message);

                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await MediaFileMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
