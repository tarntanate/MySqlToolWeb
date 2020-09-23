using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.CreateAdNetwork
{
    public class CreateAdNetworkCommandHandler : IRequestHandler<CreateAdNetworkCommand, Response<long>>
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

        public async Task<Response<long>> Handle(CreateAdNetworkCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdNetworkEntity>(request);
            await AdNetworkDbRepo.InsertAsync(entity);
            await AdNetworkDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<long>();
            return result.Success(entity.Id);
        }
    }
}
