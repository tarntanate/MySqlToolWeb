using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaUrl
{
    public class UpdateMediaUrlCommandHandler : IRequestHandler<UpdateMediaUrlCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private OokbeeAdsMongoDBRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public UpdateMediaUrlCommandHandler(
            IMediator mediator,
            OokbeeAdsMongoDBRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateMediaUrlCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request.Id, request.MediaUrl);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(string id, string fileUrl)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsResult = await Mediator.Send(new IsExistsMediaFileByIdQuery(id));
                if (!isExistsResult.Ok)
                    return isExistsResult;

                var now = MechineDateTime.Now;
                await MediaFileMongoDB.UpdateManyPartialAsync(
                    filter: f => f.Id == id,
                    update: Builders<MediaFileDocument>.Update
                            .Set(f => f.MediaUrl, fileUrl)
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
