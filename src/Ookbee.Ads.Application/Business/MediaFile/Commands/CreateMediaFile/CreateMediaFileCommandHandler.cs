using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Banner.Queries.GetByIdBanner;
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
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public CreateMediaFileCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediatr = mediatr;
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
                var campaignResult = await Mediatr.Send(new GetByIdBannerCommand(document.BannerId));
                if (!campaignResult.Ok)
                    return result.Fail(campaignResult.StatusCode, campaignResult.Message);

                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await MediaFileMongoDB.AddAsync(document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }

    }
}
