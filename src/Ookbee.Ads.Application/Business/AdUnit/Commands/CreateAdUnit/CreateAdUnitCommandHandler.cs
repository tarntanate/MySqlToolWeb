using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitByName;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

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
