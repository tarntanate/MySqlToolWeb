using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdAsset.Queries.IsExistsAdAssetByPosition;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetCommandHandler : IRequestHandler<CreateAdAssetCommand, HttpResult<long>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public CreateAdAssetCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdAssetCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdAssetCommand request)
        {
            var result = new HttpResult<long>();

            // var isExistsAdAssetByName = await Mediator.Send(new IsExistsAdAssetByPositionQuery((Position)Enum.Parse(typeof(Position), request.Position)));
            // if (isExistsAdAssetByName.Ok)
            //     return result.Fail(409, $"AdAsset '{request.Position.ToString()}' already exists.");

            var entity = Mapper
                .Map(request)
                .ToANew<AdAssetEntity>(cfg =>
                    cfg.Ignore(m => m.Ad));

            await AdAssetDbRepo.InsertAsync(entity);
            await AdAssetDbRepo.SaveChangesAsync();
            return result.Success(entity.Id);
        }
    }
}
