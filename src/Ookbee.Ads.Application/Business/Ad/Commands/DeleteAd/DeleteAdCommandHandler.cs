using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.DeleteAd
{
    public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public DeleteAdCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mediator = mediator;
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request, cancellationToken);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            await AdDbRepo.DeleteAsync(request.Id);
            await AdDbRepo.SaveChangesAsync(cancellationToken);

            return result.Success(true, request.Id, new AdEntity());
        }
    }
}
