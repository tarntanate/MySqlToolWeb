using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeCommandHandler : IRequestHandler<CreateAdUnitTypeCommand, Response<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public CreateAdUnitTypeCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdUnitTypeEntity>(request);
            await AdUnitTypeDbRepo.InsertAsync(entity);
            await AdUnitTypeDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<long>();
            return result.Success(entity.Id);
        }
    }
}
