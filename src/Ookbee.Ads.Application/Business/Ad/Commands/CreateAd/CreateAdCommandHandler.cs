using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdByName;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAd
{
    public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, HttpResult<long>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<AdEntity> AdEFCoreRepo { get; }

        public CreateAdCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<AdEntity> adEFCoreRepo)
        {
            Mediator = mediator;
            AdEFCoreRepo = adEFCoreRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdCommand request)
        {
            var result = new HttpResult<long>();
            
            var isExistsAdByName = await Mediator.Send(new IsExistsAdByNameQuery(request.Name));
            if (isExistsAdByName.Ok)
                return result.Fail(409, $"Ad '{request.Name}' already exists.");

            var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(request.AdUnitId));
            if (!isExistsAdUnitResult.Ok)
                return result.Fail(isExistsAdUnitResult.StatusCode, isExistsAdUnitResult.Message);

            var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(request.CampaignId));
            if (!isExistsCampaignResult.Ok)
                return result.Fail(isExistsCampaignResult.StatusCode, isExistsCampaignResult.Message);

            var entity = Mapper
                .Map(request)
                .ToANew<AdEntity>(cfg => 
                    cfg.Ignore(m => m.AdUnit, m => m.Campaign));

            await AdEFCoreRepo.InsertAsync(entity);
            await AdEFCoreRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
