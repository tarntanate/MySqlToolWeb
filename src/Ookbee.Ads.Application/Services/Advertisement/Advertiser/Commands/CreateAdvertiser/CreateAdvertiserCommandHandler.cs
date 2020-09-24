using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommandHandler : IRequestHandler<CreateAdvertiserCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo;

        public CreateAdvertiserCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            Mapper = mapper;
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdvertiserEntity>(request);
            await AdvertiserDbRepo.InsertAsync(entity);
            await AdvertiserDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<long>().Success(entity.Id);
        }
    }
}
