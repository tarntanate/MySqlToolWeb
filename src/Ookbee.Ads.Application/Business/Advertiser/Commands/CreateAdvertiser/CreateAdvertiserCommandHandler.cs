using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserByName;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommandHandler : IRequestHandler<CreateAdvertiserCommand, HttpResult<long>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<AdvertiserEntity> AdvertiserEFCoreRepo { get; }

        public CreateAdvertiserCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<AdvertiserEntity> advertiserEFCoreRepo)
        {
            Mediator = mediator;
            AdvertiserEFCoreRepo = advertiserEFCoreRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdvertiserCommand request)
        {
            var result = new HttpResult<long>();

            var isExistsAdvertiserByName = await Mediator.Send(new IsExistsAdvertiserByNameQuery(request.Name));
            if (isExistsAdvertiserByName.Ok)
                return result.Fail(409, $"Advertiser '{request.Name}' already exists.");

            var entity = Mapper
                .Map(request)
                .ToANew<AdvertiserEntity>();

            await AdvertiserEFCoreRepo.InsertAsync(entity);
            await AdvertiserEFCoreRepo.SaveChangesAsync();
            
            return result.Success(entity.Id);
        }
    }
}
