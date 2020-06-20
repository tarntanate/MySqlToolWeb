using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommandHandler : IRequestHandler<CreateAdvertiserCommand, HttpResult<long>>
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

        public async Task<HttpResult<long>> Handle(CreateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdvertiserCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<AdvertiserEntity>(request);
            await AdvertiserDbRepo.InsertAsync(entity);
            await AdvertiserDbRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
