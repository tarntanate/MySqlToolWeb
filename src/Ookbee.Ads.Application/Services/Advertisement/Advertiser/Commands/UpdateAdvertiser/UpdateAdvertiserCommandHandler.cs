using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Commands.UpdateAdvertiser
{
    public class UpdateAdvertiserCommandHandler : IRequestHandler<UpdateAdvertiserCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo;

        public UpdateAdvertiserCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            Mapper = mapper;
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdvertiserEntity>(request);
            await AdvertiserDbRepo.UpdateAsync(entity.Id, entity);
            await AdvertiserDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
