using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaUrl
{
    public class UpdateMediaUrlCommandHandler : IRequestHandler<UpdateMediaUrlCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public UpdateMediaUrlCommandHandler(
            IMediator mediator,
            AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateMediaUrlCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdateMediaUrlCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsResult = await Mediator.Send(new IsExistsMediaFileByIdQuery(request.Id));
                if (!isExistsResult.Ok)
                    return isExistsResult;

                var now = MechineDateTime.Now;
                await MediaFileMongoDB.UpdateManyPartialAsync(
                    filter: f => f.Id == request.Id,
                    update: Builders<MediaFileDocument>.Update
                            .Set(f => f.MediaUrl, request.MediaUrl)
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
