using COSXML.Model.Object;
using COSXML.Model.Tag;
using COSXML.Utils;
using MediatR;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.InitializeCosXmlServer;
using Ookbee.Ads.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure.Tencent.Cos.CopyObject
{
    public class CopyObjectCommandHandler : IRequestHandler<CopyObjectCommand, bool>
    {
        private IMediator Mediator { get; }

        public CopyObjectCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<bool> Handle(CopyObjectCommand request, CancellationToken cancellationToken)
        {
            var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
            var cosXml = await Mediator.Send(new InitializeCosXmlServerCommand());
            
            var sourceAppid = request.SourceAppid; 
            var sourceRegion = request.SourceRegion; 
            var sourceBucket = request.SourceBucket; 
            var sourceKey = request.SourceKey; 
            var copySourceStruct = new CopySourceStruct(sourceAppid, sourceBucket, sourceRegion, sourceKey);

            var destinationBucket = request.DestinationBucket; 
            var destinationKey = request.DestinationKey;
            var copyObjectRequest = new CopyObjectRequest(destinationBucket, destinationKey);

            copyObjectRequest.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            copyObjectRequest.SetCopySource(copySourceStruct);
            copyObjectRequest.SetCopyMetaDataDirective(COSXML.Common.CosMetaDataDirective.COPY);
            var copyObjectResult = cosXml.CopyObject(null);
            return copyObjectResult.httpCode == 200 ? true : false;
        }
    }
}
