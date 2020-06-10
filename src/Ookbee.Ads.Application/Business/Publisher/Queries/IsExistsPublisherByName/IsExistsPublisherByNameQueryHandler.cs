using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherByName
{
    public class IsExistsPublisherByNameQueryHandler : IRequestHandler<IsExistsPublisherByNameQuery, HttpResult<bool>>
    {
        private AdsEFCoreRepository<PublisherEntity> PublisherEFCoreRepo { get; }

        public IsExistsPublisherByNameQueryHandler(AdsEFCoreRepository<PublisherEntity> publisherEFCoreRepo)
        {
            PublisherEFCoreRepo = publisherEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsPublisherByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsPublisherByNameQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await PublisherEFCoreRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            if (!isExists)
                return result.Fail(404, $"Publisher '{request.Name}' doesn't exist.");
            return result.Success(true);
        }
    }
}
