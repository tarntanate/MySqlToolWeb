using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Commands.UpdateAdUnitType
{
    public class UpdateAdUnitTypeCommandHandler : IRequestHandler<UpdateAdUnitTypeCommand, Response<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public UpdateAdUnitTypeCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdUnitTypeEntity>(request);
            await AdUnitTypeDbRepo.UpdateAsync(entity.Id, entity);
            await AdUnitTypeDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
