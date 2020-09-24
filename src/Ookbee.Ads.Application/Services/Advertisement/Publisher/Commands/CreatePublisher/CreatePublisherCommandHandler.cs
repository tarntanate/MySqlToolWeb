using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.CreatePublisher
{
    public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<PublisherEntity> PublisherDbRepo;

        public CreatePublisherCommandHandler(
            IMapper mapper,
            AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            Mapper = mapper;
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<Response<long>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<PublisherEntity>(request);
            await PublisherDbRepo.InsertAsync(entity);
            await PublisherDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<long>().Success(entity.Id);
        }
    }
}
