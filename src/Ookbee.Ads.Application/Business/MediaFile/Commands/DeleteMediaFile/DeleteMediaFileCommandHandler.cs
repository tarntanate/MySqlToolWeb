using MediatR;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetByIdMediaFile;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.DeleteMediaFile
{
    public class DeleteMediaFileCommandHandler : IRequestHandler<DeleteMediaFileCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public DeleteMediaFileCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediatr = mediatr;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteMediaFileCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request.Id);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(string id)
        {
            var result = new HttpResult<bool>();
            
            var BannerResult = await Mediatr.Send(new GetByIdMediaFileCommand(id));
            if (!BannerResult.Ok)
                return result.Fail(BannerResult.StatusCode, BannerResult.Message);

            await MediaFileMongoDB.DeleteAsync(id);
            return result.Success(true);
        }
    }
}
