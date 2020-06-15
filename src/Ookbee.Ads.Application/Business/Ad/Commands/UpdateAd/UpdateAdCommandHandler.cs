using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdByName;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd
{
    public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public UpdateAdCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mediator = mediator;
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateAdCommand request)
        {
            var result = new HttpResult<bool>();

            var adResult = await Mediator.Send(new GetAdByIdQuery(request.Id));
            if (!adResult.Ok)
                return result.Fail(adResult.StatusCode, adResult.Message);

            var adByNameResult = await Mediator.Send(new GetAdByNameQuery(request.Name));
            if (adByNameResult.Ok &&
                adByNameResult.Data.Id != request.Id &&
                adByNameResult.Data.Name == request.Name)
                return result.Fail(409, $"Ad '{request.Name}' already exists.");

            var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(request.AdUnitId));
            if (!isExistsAdUnitResult.Ok)
                return result.Fail(isExistsAdUnitResult.StatusCode, isExistsAdUnitResult.Message);

            var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(request.CampaignId));
            if (!isExistsCampaignResult.Ok)
                return result.Fail(isExistsCampaignResult.StatusCode, isExistsCampaignResult.Message);

            var source = Mapper
                .Map(request)
                .Over(adResult.Data);
            var entity = Mapper
                .Map(source)
                .ToANew<AdEntity>();

            await AdDbRepo.UpdateAsync(entity.Id, entity);
            await AdDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
