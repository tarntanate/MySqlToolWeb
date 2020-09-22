using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommandHandler : IRequestHandler<CreateAdvertiserCommand, Response<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public CreateAdvertiserCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdvertiserEntity>(request);
            await AdvertiserDbRepo.InsertAsync(entity);
            await AdvertiserDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<long>();
            return result.Success(entity.Id);
        }
    }
}
