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
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public CreatePublisherCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<Response<long>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<PublisherEntity>(request);
            await PublisherDbRepo.InsertAsync(entity);
            await PublisherDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<long>();
            return result.Success(entity.Id);
        }
    }
}
