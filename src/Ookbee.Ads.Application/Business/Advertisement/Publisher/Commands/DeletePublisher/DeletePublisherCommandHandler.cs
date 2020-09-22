using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Commands.DeletePublisher
{
    public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, Response<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public DeletePublisherCommandHandler(
            IMediator mediator,
            AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            Mediator = mediator;
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<Response<bool>> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            await PublisherDbRepo.DeleteAsync(request.Id);
            await PublisherDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
