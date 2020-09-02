using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.CreateAdAssetCache;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetCommandHandler : IRequestHandler<CreateAdAssetCommand, HttpResult<long>>
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

        public async Task<HttpResult<long>> Handle(CreateAdAssetCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdAssetEntity>(request);
            await AdAssetDbRepo.InsertAsync(entity);
            await AdAssetDbRepo.SaveChangesAsync(cancellationToken);
            await Mediator.Send(new CreateAdAssetCacheCommand(entity.AdId), cancellationToken);

            var result = new HttpResult<long>();
            return result.Success(entity.Id);
        }
    }
}
