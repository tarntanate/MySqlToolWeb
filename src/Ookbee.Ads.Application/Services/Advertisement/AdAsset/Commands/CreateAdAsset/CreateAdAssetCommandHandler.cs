using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.CeateAdCacheByAssetId;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetCommandHandler : IRequestHandler<CreateAdAssetCommand, Response<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public CreateAdAssetCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdAssetCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdAssetEntity>(request);
            await AdAssetDbRepo.InsertAsync(entity);
            await AdAssetDbRepo.SaveChangesAsync(cancellationToken);
            await Mediator.Send(new CreateAdCacheByAssetIdCommand(entity.Id), cancellationToken);

            var result = new Response<long>();
            return result.Success(entity.Id);
        }
    }
}
