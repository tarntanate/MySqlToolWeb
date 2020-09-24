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
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo;

        public CreateAdUnitTypeCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            Mapper = mapper;
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdUnitTypeEntity>(request);
            await AdUnitTypeDbRepo.InsertAsync(entity);
            await AdUnitTypeDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<long>().Success(entity.Id);
        }
    }
}
