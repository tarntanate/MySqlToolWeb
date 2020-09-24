using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdGroupCache.Commands.CreateAdGroupCache;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.CreateAdGroup
{
    public class CreateAdGroupCommandHandler : IRequestHandler<CreateAdGroupCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly IMediator Mediator;
        private readonly AdsDbRepository<AdGroupEntity> AdGroupDbRepo;

        public CreateAdGroupCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupEntity>(request);
            await AdGroupDbRepo.InsertAsync(entity);
            await AdGroupDbRepo.SaveChangesAsync(cancellationToken);
            await Mediator.Send(new CreateAdGroupCacheCommand(entity.Id), cancellationToken);
            return new Response<long>().Success(entity.Id);
        }
    }
}
