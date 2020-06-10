using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.DeleteAd
{
    public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<AdEntity> AdEFCoreRepo { get; }

        public DeleteAdCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<AdEntity> adEFCoreRepo)
        {
            Mediator = mediator;
            AdEFCoreRepo = adEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteAdCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsAdByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await AdEFCoreRepo.DeleteAsync(request.Id);
            await AdEFCoreRepo.SaveChangesAsync();
            
            return result.Success(true);
        }
    }
}
