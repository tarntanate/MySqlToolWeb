using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.CreateAdCache;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.CreateAd
{
    public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, Response<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public CreateAdCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdEntity>(request);
            await AdDbRepo.InsertAsync(entity);
            await AdDbRepo.SaveChangesAsync(cancellationToken);
            await Mediator.Send(new CreateAdCacheCommand(entity.Id), cancellationToken);

            var result = new Response<long>();
            return result.Success(entity.Id);
        }
    }
}
