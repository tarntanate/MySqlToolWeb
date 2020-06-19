using System.Threading;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitByName;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.IsExistsAdUnitTypeById;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommandHandler : IRequestHandler<CreateAdUnitCommand, HttpResult<long>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public CreateAdUnitCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdUnitCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdUnitCommand request)
        {
            var result = new HttpResult<long>();

            var isExistsAdUnitTypeById = await Mediator.Send(new IsExistsAdUnitTypeByIdQuery(request.AdUnitTypeId));
            if (!isExistsAdUnitTypeById.Ok)
                return result.Fail(isExistsAdUnitTypeById.StatusCode, isExistsAdUnitTypeById.Message);

            var isExistsPublisherById = await Mediator.Send(new IsExistsPublisherByIdQuery(request.PublisherId));
            if (!isExistsPublisherById.Ok)
                return result.Fail(isExistsPublisherById.StatusCode, isExistsPublisherById.Message);

            var isExistsAdUnitByName = await Mediator.Send(new IsExistsAdUnitByNameQuery(request.Name));
            if (isExistsAdUnitByName.Ok)
                return result.Fail(409, $"AdUnit '{request.Name}' already exists.");

            var entity = Mapper
                .Map(request)
                .ToANew<AdUnitEntity>(cfg =>
                   cfg.Ignore(m => m.AdUnitType, m => m.Publisher));

            await AdUnitDbRepo.InsertAsync(entity);
            await AdUnitDbRepo.SaveChangesAsync();
            return result.Success(entity.Id);
        }
    }
}
