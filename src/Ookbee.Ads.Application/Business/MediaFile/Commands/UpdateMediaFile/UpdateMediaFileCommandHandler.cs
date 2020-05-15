using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileById;
using Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFile
{
    public class UpdateMediaFileCommandHandler : IRequestHandler<UpdateMediaFileCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private OokbeeAdsMongoDBRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public UpdateMediaFileCommandHandler(
            IMediator mediator,
            OokbeeAdsMongoDBRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateMediaFileCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<MediaFileDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(MediaFileDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsResult = await Mediator.Send(new IsExistsMediaFileByIdQuery(document.Id));
                if (!isExistsResult.Ok)
                    return isExistsResult;

                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await MediaFileMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
