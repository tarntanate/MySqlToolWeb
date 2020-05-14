using COSXML.Model.Tag;
using MediatR;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.InitializeCosXmlServer;
using Ookbee.Ads.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure.Tencent.Cos
{
    public class GenerateSignURLCommandHandler : IRequestHandler<GenerateSignURLCommand, string>
    {
        private IMediator Mediator { get; }

        public GenerateSignURLCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<string> Handle(GenerateSignURLCommand request, CancellationToken cancellationToken)
        {
            var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
            var cosXml = await Mediator.Send(new InitializeCosXmlServerCommand());
            var preSignatureStruct = new PreSignatureStruct();
            preSignatureStruct.appid = cosConfig.AppId;                             //Tencent Cloud account's APPID
            preSignatureStruct.region = cosConfig.Region;                           //Region of the bucket
            preSignatureStruct.bucket = request.Bucket;                             //Bucket
            preSignatureStruct.key = request.Key;                                   //ObjectKey
            preSignatureStruct.httpMethod = "PUT";                                  //HTTP request method
            preSignatureStruct.isHttps = cosConfig.IsHttps;                         //Generate HTTPS request URL
            preSignatureStruct.signDurationSecond = cosConfig.DurationSecond;       //Validity period of request signature is 600s
            preSignatureStruct.headers = request.Headers;                           //The header in signature for verification
            preSignatureStruct.queryParameters = request.QueryParameters;           //The request parameters of URL in signature for verification
            var signURL = cosXml.GenerateSignURL(preSignatureStruct);               //The pre-signed URL (a signature URL computed with permanent key) for upload request
            return signURL;
        }
    }
}
