using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, Response<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public UpdatePublisherCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<PublisherEntity>(request);
            await PublisherDbRepo.UpdateAsync(entity.Id, entity);
            await PublisherDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
