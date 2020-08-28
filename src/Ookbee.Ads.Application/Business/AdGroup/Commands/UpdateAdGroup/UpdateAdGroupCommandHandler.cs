using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.UpdateAdGroupCache;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupCommandHandler : IRequestHandler<UpdateAdGroupCommand, HttpResult<bool>>
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

        public async Task<HttpResult<bool>> Handle(UpdateAdGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupEntity>(request);
            await Mediator.Send(new UpdateAdGroupCacheCommand(adGroupId: entity.Id), cancellationToken);
            await AdGroupDbRepo.UpdateAsync(entity.Id, entity);
            await AdGroupDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<bool>();
            return result.Success(true);
        }
    }
}
