using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.UpdateAdCacheByAssetId;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetCommandHandler : IRequestHandler<UpdateAdAssetCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly IMediator Mediator;
        private readonly AdsDbRepository<AdAssetEntity> AdAssetDbRepo;

        public UpdateAdAssetCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdAssetCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdAssetEntity>(request);
            await AdAssetDbRepo.UpdateAsync(entity.Id, entity);
            await AdAssetDbRepo.SaveChangesAsync(cancellationToken);
            await Mediator.Send(new UpdateAdCacheByAssetIdCommand(entity.Id), cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
