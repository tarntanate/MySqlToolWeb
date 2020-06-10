using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherById
{
    public class GetPublisherByIdQueryHandler : IRequestHandler<GetPublisherByIdQuery, HttpResult<PublisherDto>>
    {
        private AdsEFCoreRepository<PublisherEntity> PublisherEFCoreRepo { get; }

        public GetPublisherByIdQueryHandler(AdsEFCoreRepository<PublisherEntity> publisherEFCoreRepo)
        {
            PublisherEFCoreRepo = publisherEFCoreRepo;
        }

        public async Task<HttpResult<PublisherDto>> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<PublisherDto>> GetOnDb(GetPublisherByIdQuery request)
        {
            var result = new HttpResult<PublisherDto>();

            var item = await PublisherEFCoreRepo.FirstAsync(filter: f => 
                f.Id == request.Id && 
                f.DeletedAt == null
            );
            
            if (item == null)
                return result.Fail(404, $"Publisher '{request.Id}' doesn't exist.");
                
            var data = Mapper
                .Map(item)
                .ToANew<PublisherDto>();

            return result.Success(data);
        }
    }
}
