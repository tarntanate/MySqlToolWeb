using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdGroupCache.Commands.UpdateAdGroupCache;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupCommandHandler : IRequestHandler<UpdateAdGroupCommand, Response<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public UpdateAdGroupCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupEntity>(request);
            await Mediator.Send(new UpdateAdGroupCacheCommand(entity.Id), cancellationToken);
            await AdGroupDbRepo.UpdateAsync(entity.Id, entity);
            await AdGroupDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
