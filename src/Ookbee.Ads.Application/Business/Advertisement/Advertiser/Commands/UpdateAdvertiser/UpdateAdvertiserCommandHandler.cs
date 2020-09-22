using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.Advertiser.Commands.UpdateAdvertiser
{
    public class UpdateAdvertiserCommandHandler : IRequestHandler<UpdateAdvertiserCommand, Response<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public UpdateAdvertiserCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdvertiserEntity>(request);
            await AdvertiserDbRepo.UpdateAsync(entity.Id, entity);
            await AdvertiserDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
