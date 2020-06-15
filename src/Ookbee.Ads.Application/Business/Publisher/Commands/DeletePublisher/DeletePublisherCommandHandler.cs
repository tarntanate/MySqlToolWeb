using MediatR;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.DeletePublisher
{
    public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, HttpResult<bool>>
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

        public async Task<HttpResult<bool>> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeletePublisherCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsPublisherByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await PublisherDbRepo.DeleteAsync(request.Id);
            await PublisherDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
