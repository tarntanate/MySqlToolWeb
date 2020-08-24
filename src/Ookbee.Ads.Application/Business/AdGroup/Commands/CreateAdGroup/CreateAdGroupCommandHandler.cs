using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroupCache.Commands.CreateAdGroupCache;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroup.Commands.CreateAdGroup
{
    public class CreateAdGroupCommandHandler : IRequestHandler<CreateAdGroupCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public CreateAdGroupCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupEntity>(request);
            await AdGroupDbRepo.InsertAsync(entity);
            await AdGroupDbRepo.SaveChangesAsync(cancellationToken);
            await Mediator.Send(new CreateAdGroupCacheCommand(entity.Id), cancellationToken);

            var result = new HttpResult<long>();
            return result.Success(entity.Id);
        }
    }
}
