using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.IsExistsPublisherById
{
    public class IsExistsPublisherByIdQueryHandler : IRequestHandler<IsExistsPublisherByIdQuery, Response<bool>>
    {
        private readonly AdsDbRepository<PublisherEntity> PublisherDbRepo;

        public IsExistsPublisherByIdQueryHandler(AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsPublisherByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await PublisherDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
