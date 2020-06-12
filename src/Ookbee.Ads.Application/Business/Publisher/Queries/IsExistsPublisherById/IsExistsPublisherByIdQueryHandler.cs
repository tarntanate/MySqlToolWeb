using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById
{
    public class IsExistsPublisherByIdQueryHandler : IRequestHandler<IsExistsPublisherByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public IsExistsPublisherByIdQueryHandler(AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsPublisherByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsPublisherByIdQuery request)
        {
            var result = new HttpResult<bool>();
            
            var isExists = await PublisherDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            if (!isExists)
                return result.Fail(404, $"Publisher '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
