using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommandHandler : IRequestHandler<DeleteAdvertiserCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public DeleteAdvertiserCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            Mediator = mediator;
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteAdvertiserCommand request)
        {
            var result = new HttpResult<bool>();

            await AdvertiserDbRepo.DeleteAsync(request.Id);
            await AdvertiserDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
