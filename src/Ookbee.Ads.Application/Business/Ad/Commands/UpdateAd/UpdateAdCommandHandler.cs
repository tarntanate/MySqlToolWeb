using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.UpdateAdAssetCache;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd
{
    public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public UpdateAdCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdEntity>(request);
            await AdDbRepo.UpdateAsync(entity.Id, entity);
            await AdDbRepo.SaveChangesAsync(cancellationToken);
            await Mediator.Send(new UpdateAdAssetCacheCommand(entity.Id), cancellationToken);

            var result = new HttpResult<bool>();
            return result.Success(true);
        }
    }
}
