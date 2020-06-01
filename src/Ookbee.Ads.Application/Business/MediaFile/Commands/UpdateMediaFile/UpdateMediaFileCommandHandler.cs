using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByPosition;
using Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById;
using Ookbee.Ads.Common;
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
                var isExistsMediaFileResult = await Mediator.Send(new IsExistsMediaFileByIdQuery(request.Id));
                if (!isExistsMediaFileResult.Ok)
                    return isExistsMediaFileResult;

                var mediaFileResult = await Mediator.Send(new GetMediaFileByPositionQuery(request.AdId, request.Position));
                if (mediaFileResult.Ok &&
                    mediaFileResult.Data.Id != request.Id &&
                    mediaFileResult.Data.Position == request.Position)
                    return result.Fail(409, $"MediaFile '{request.Position}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(mediaFileResult.Data).ToANew<MediaFileDocument>();
                document.Position = request.Position;
                document.UpdatedDate = now.DateTime;
                await MediaFileMongoDB.UpdateAsync(request.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return result.Fail(500, ex.Message);
            }
        }
    }
}
