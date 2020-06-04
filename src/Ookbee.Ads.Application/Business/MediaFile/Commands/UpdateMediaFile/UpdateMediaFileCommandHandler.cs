using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileById;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByPosition;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFile
{
    public class UpdateMediaFileCommandHandler : IRequestHandler<UpdateMediaFileCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public UpdateMediaFileCommandHandler(
            IMediator mediator,
            AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateMediaFileCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdateMediaFileCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var mediaFileResult = await Mediator.Send(new GetMediaFileByIdQuery(request.Id));
                if (!mediaFileResult.Ok)
                    return result.Fail(mediaFileResult.StatusCode, mediaFileResult.Message);

                var mediaFileByPositionResult = await Mediator.Send(new GetMediaFileByPositionQuery(request.AdId, request.Position));
                if (mediaFileByPositionResult.Ok &&
                    mediaFileByPositionResult.Data.Id != request.Id &&
                    mediaFileByPositionResult.Data.Position == request.Position)
                    return result.Fail(409, $"MediaFile '{request.Position}' already exists.");

                var template = Mapper.Map(request).Over(mediaFileResult.Data);
                var document = Mapper.Map(template).ToANew<MediaFileDocument>();
                await MediaFileMongoDB.UpdateAsync(request.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
