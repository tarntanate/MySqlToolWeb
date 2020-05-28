using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById;
using Ookbee.Ads.Application.Business.UploadUrl.Queries.GetUploadUrlById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateUploadUrl
{
    public class UpdateUploadUrlCommandHandler : IRequestHandler<UpdateUploadUrlCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public UpdateUploadUrlCommandHandler(
            IMediator mediator,
            AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateUploadUrlCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdateUploadUrlCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsResult = await Mediator.Send(new IsExistsMediaFileByIdQuery(request.Id));
                if (!isExistsResult.Ok)
                    return isExistsResult;

                var uploadUrlResult = await Mediator.Send(new GetUploadUrlByIdQuery(request.UploadUrlId));
                if (!uploadUrlResult.Ok)
                    return result.Fail(uploadUrlResult.StatusCode, uploadUrlResult.Message);

                var now = MechineDateTime.Now;
                await MediaFileMongoDB.UpdateManyPartialAsync(
                    filter: f => f.Id == request.Id,
                    update: Builders<MediaFileDocument>.Update
                            .Set(f => f.MediaUrl, uploadUrlResult.Data.DestinationKey)
                            .Set(f => f.MimeType, uploadUrlResult.Data.MapperType)
                            .Set(f => f.UpdatedDate, now.DateTime)
                );
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
