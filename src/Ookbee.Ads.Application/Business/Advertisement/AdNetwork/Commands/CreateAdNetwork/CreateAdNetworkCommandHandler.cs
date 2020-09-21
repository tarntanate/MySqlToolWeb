using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Commands.CreateAdNetwork
{
    public class CreateAdNetworkCommandHandler : IRequestHandler<CreateAdNetworkCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo { get; }

        public CreateAdNetworkCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdNetworkCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdNetworkEntity>(request);
            await AdNetworkDbRepo.InsertAsync(entity);
            await AdNetworkDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<long>();
            return result.Success(entity.Id);
        }
    }
}
