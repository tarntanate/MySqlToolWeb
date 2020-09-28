using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommandHandler : IRequestHandler<DeleteAdvertiserCommand, Response<bool>>
    {
        private readonly AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo;

        public DeleteAdvertiserCommandHandler(
            AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteAdvertiserCommand request, CancellationToken cancellationToken)
        {
            await AdvertiserDbRepo.DeleteAsync(request.Id);
            await AdvertiserDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
