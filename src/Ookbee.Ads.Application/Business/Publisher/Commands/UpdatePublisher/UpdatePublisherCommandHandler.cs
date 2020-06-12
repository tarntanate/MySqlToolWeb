using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherByName;
using Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;
using Ookbee.Ads.Domain.Entities;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public UpdatePublisherCommandHandler(
            IMediator mediator,
            AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            Mediator = mediator;
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdatePublisherCommand request)
        {
            var result = new HttpResult<bool>();

            var publisherResult = await Mediator.Send(new GetPublisherByIdQuery(request.Id));
            if (!publisherResult.Ok)
                return result.Fail(publisherResult.StatusCode, publisherResult.Message);

            var publisherByNameResult = await Mediator.Send(new GetPublisherByNameQuery(request.Name));
            if (publisherByNameResult.Ok &&
                publisherByNameResult.Data.Id != request.Id &&
                publisherByNameResult.Data.Name == request.Name)
                return result.Fail(409, $"Publisher '{request.Name}' already exists.");

            var source = Mapper
                .Map(request)
                .Over(publisherResult.Data);
            var entity = Mapper
                .Map(source)
                .ToANew<PublisherEntity>();

            await PublisherDbRepo.UpdateAsync(entity.Id, entity);
            await PublisherDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
