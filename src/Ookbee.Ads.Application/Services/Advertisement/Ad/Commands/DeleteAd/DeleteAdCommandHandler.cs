using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.DeleteAdCache;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.DeleteAd
{
    public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, Response<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public DeleteAdCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mediator = mediator;
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();

            await AdDbRepo.DeleteAsync(request.Id);
            await AdDbRepo.SaveChangesAsync(cancellationToken);

            await Mediator.Send(new DeleteAdCacheCommand(request.Id), cancellationToken);

            return result.Success(true);
        }
    }
}
