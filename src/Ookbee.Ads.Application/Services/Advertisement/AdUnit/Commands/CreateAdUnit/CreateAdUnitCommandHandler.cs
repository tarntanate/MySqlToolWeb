using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.CreateAdUnitCacheGroupId;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommandHandler : IRequestHandler<CreateAdUnitCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly IMediator Mediator;
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public CreateAdUnitCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdUnitCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdUnitEntity>(request);
            await AdUnitDbRepo.InsertAsync(entity);
            await AdUnitDbRepo.SaveChangesAsync(cancellationToken);
            await Mediator.Send(new CreateAdUnitCacheByGroupIdCommand(entity.AdGroupId), cancellationToken);
            return new Response<long>().OK(entity.Id);
        }
    }
}
