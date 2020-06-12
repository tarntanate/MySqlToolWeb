using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.IsExistsAdUnitTypeByName;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeCommandHandler : IRequestHandler<CreateAdUnitTypeCommand, HttpResult<long>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public CreateAdUnitTypeCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            Mediator = mediator;
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdUnitTypeCommand request)
        {
            var result = new HttpResult<long>();

            var isExistsAdUnitTypeByName = await Mediator.Send(new IsExistsAdUnitTypeByNameQuery(request.Name));
            if (isExistsAdUnitTypeByName.Ok)
                return result.Fail(409, $"AdUnitType '{request.Name}' already exists.");

            var entity = Mapper
                .Map(request)
                .ToANew<AdUnitTypeEntity>();

            await AdUnitTypeDbRepo.InsertAsync(entity);
            await AdUnitTypeDbRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
