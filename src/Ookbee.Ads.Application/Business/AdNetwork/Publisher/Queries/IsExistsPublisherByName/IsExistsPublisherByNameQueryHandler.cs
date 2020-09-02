using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Publisher.Queries.IsExistsPublisherByName
{
    public class IsExistsPublisherByNameQueryHandler : IRequestHandler<IsExistsPublisherByNameQuery, HttpResult<bool>>
    {
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public IsExistsPublisherByNameQueryHandler(AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsPublisherByNameQuery request, CancellationToken cancellationToken)
        {
            var isExists = await PublisherDbRepo.AnyAsync(f =>
                f.Name == request.Name &&
                f.DeletedAt == null
            );

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"Publisher '{request.Name}' doesn't exist.");
        }
    }
}
