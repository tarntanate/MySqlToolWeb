using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByName;
using Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById;
using Ookbee.Ads.Common.Helpers;
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
            var document = Mapper.Map(request).ToANew<MediaFileDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(MediaFileDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsMediaFileResult = await Mediator.Send(new IsExistsMediaFileByIdQuery(document.Id));
                if (!isExistsMediaFileResult.Ok)
                    return isExistsMediaFileResult;
                    
                var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(document.Id));
                if (!isExistsAdResult.Ok)
                    return isExistsAdResult;

                var mediaFileResult = await Mediator.Send(new GetMediaFileByNameQuery(document.Name));
                if (mediaFileResult.Ok &&
                    mediaFileResult.Data.Id != document.Id &&
                    mediaFileResult.Data.Name == document.Name)
                    return result.Fail(409, $"MediaFile '{document.Name}' already exists.");

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
