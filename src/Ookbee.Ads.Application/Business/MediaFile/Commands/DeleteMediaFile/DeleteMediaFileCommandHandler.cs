using System.Runtime.Serialization;
using System;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.DeleteMediaFile
{
    public class DeleteMediaFileCommandHandler : IRequestHandler<DeleteMediaFileCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public DeleteMediaFileCommandHandler(
            IMediator mediator,
            AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteMediaFileCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(DeleteMediaFileCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(request.AdId));
            if (!isExistsAdResult.Ok)
                return isExistsAdResult;

            var isExistsMediaFileResult = await Mediator.Send(new IsExistsMediaFileByIdQuery(request.Id));
            if (!isExistsMediaFileResult.Ok)
                return isExistsMediaFileResult;

            await MediaFileMongoDB.UpdateManyPartialAsync(
                filter: f => f.Id == request.Id, 
                update: Builders<MediaFileDocument>.Update.Set(f => f.EnabledFlag, false)
            );
            return result.Success(true);
        }
    }
}
