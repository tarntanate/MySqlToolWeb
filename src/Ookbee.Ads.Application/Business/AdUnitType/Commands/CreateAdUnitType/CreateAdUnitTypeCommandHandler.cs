using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.IsExistsAdUnitTypeByName;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeCommandHandler : IRequestHandler<CreateAdUnitTypeCommand, HttpResult<long>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<AdUnitTypeEntity> AdUnitTypeEFCoreRepo { get; }

        public CreateAdUnitTypeCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<AdUnitTypeEntity> adUnitTypeEFCoreRepo)
        {
            Mediator = mediator;
            AdUnitTypeEFCoreRepo = adUnitTypeEFCoreRepo;
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

            await AdUnitTypeEFCoreRepo.InsertAsync(entity);
            await AdUnitTypeEFCoreRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
