using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.DeletePublisher
{
    public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<PublisherEntity> PublisherEFCoreRepo { get; }

        public DeletePublisherCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<PublisherEntity> publisherEFCoreRepo)
        {
            Mediator = mediator;
            PublisherEFCoreRepo = publisherEFCoreRepo;
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

            await PublisherEFCoreRepo.DeleteAsync(request.Id);
            await PublisherEFCoreRepo.SaveChangesAsync();
            
            return result.Success(true);
        }
    }
}
