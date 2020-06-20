using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetCommandHandler : IRequestHandler<UpdateAdAssetCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public UpdateAdAssetCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdAssetCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateAdAssetCommand request)
        {
            var result = new HttpResult<bool>();

            var entity = Mapper.Map<AdAssetEntity>(request);
            await AdAssetDbRepo.UpdateAsync(entity.Id, entity);
            await AdAssetDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
