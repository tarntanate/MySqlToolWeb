using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitByName;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;
using Ookbee.Ads.Domain.Entities;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitCommandHandler : IRequestHandler<UpdateAdUnitCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<AdUnitEntity> AdUnitEFCoreRepo { get; }

        public UpdateAdUnitCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<AdUnitEntity> adUnitEFCoreRepo)
        {
            Mediator = mediator;
            AdUnitEFCoreRepo = adUnitEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdUnitCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateAdUnitCommand request)
        {
            var result = new HttpResult<bool>();
            
            var adUnitResult = await Mediator.Send(new GetAdUnitByIdQuery(request.Id));
            if (!adUnitResult.Ok)
                return result.Fail(adUnitResult.StatusCode, adUnitResult.Message);

            var adUnitByNameResult = await Mediator.Send(new GetAdUnitByNameQuery(request.Name));
            if (adUnitByNameResult.Ok &&
                adUnitByNameResult.Data.Id != request.Id &&
                adUnitByNameResult.Data.Name == request.Name)
                return result.Fail(409, $"AdUnit '{request.Name}' already exists.");

            var source = Mapper
                .Map(request)
                .Over(adUnitResult.Data);
            var entity = Mapper
                .Map(source)
                .ToANew<AdUnitEntity>(cfg => 
                    cfg.Ignore(m => m.AdUnitType, m => m.Publisher));

            await AdUnitEFCoreRepo.UpdateAsync(entity.Id, entity);
            await AdUnitEFCoreRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
