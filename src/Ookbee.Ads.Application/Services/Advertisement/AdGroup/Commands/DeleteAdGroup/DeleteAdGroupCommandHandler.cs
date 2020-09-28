using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdGroupCache.Commands.DeleteAdGroupCache;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.DeleteAdGroup
{
    public class DeleteAdGroupCommandHandler : IRequestHandler<DeleteAdGroupCommand, Response<bool>>
    {
        private readonly IMediator Mediator;
        private readonly AdsDbRepository<AdGroupEntity> AdGroupDbRepo;

        public DeleteAdGroupCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteAdGroupCommand request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteAdGroupCacheCommand(request.Id), cancellationToken);
            await AdGroupDbRepo.DeleteAsync(request.Id);
            await AdGroupDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
