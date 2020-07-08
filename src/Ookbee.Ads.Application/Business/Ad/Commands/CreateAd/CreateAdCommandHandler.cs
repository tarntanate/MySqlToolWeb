using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAd
{
    public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, HttpResult<long>>
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

        public async Task<HttpResult<long>> Handle(CreateAdCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request, cancellationToken);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<AdEntity>(request);
            await AdDbRepo.InsertAsync(entity);
            await AdDbRepo.SaveChangesAsync(cancellationToken);

            return result.Success(entity.Id, entity.Id, entity);
        }
    }
}
