using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Commands.UpdateAdUnitType
{
    public class UpdateAdUnitTypeCommandHandler : IRequestHandler<UpdateAdUnitTypeCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo;

        public UpdateAdUnitTypeCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            Mapper = mapper;
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdUnitTypeEntity>(request);
            await AdUnitTypeDbRepo.UpdateAsync(entity.Id, entity);
            await AdUnitTypeDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
