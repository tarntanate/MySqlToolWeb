using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdCache.Commands.UpdateAdCache;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusCommandHandler : IRequestHandler<UpdateAdStatusCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public UpdateAdStatusCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdEntity>(request);
            await AdDbRepo.UpdateAsync(entity.Id, entity, x => x.Status);
            await AdDbRepo.SaveChangesAsync(cancellationToken);
            await Mediator.Send(new UpdateAdCacheCommand(entity.Id), cancellationToken);

            var result = new HttpResult<bool>();
            return result.Success(true);
        }
    }
}
