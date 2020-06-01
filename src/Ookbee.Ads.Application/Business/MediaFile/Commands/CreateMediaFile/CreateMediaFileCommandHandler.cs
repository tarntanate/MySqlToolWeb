using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByPosition;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.CreateMediaFile
{
    public class CreateMediaFileCommandHandler : IRequestHandler<CreateMediaFileCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public CreateMediaFileCommandHandler(
            IMediator mediator,
            AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateMediaFileCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CreateMediaFileCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
                var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(request.AdId));
                if (!isExistsAdResult.Ok)
                    return result.Fail(isExistsAdResult.StatusCode, isExistsAdResult.Message);

                var mediaFileResult = await Mediator.Send(new GetMediaFileByPositionQuery(request.AdId, request.Position));
                if (mediaFileResult.Ok &&
                    mediaFileResult.Data.Id != request.Id &&
                    mediaFileResult.Data.Position == request.Position)
                    return result.Fail(409, $"MediaFile '{request.Position}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<MediaFileDocument>();
                document.MediaUrl = null;
                document.MimeType = null;
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
