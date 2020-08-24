using COSXML;
using COSXML.Auth;
using MediatR;
using Ookbee.Ads.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure.Tencent.Cos.InitializeCosXmlServer
{
    public class InitializeCosXmlServerCommandHandler : IRequestHandler<InitializeCosXmlServerCommand, CosXmlServer>
    {
        public async Task<CosXmlServer> Handle(InitializeCosXmlServerCommand request, CancellationToken cancellationToken)
        {
            var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
            var cosXmlServer = await Task.Run(() =>
            {
                var cosXmlConfig = new CosXmlConfig.Builder()
                .SetConnectionTimeoutMs(cosConfig.ConnectionTimeoutMs)                  //Set the connection timeout threshold (in ms). Defaults to 45000 ms.
                .SetReadWriteTimeoutMs(cosConfig.ReadWriteTimeoutMs)                    //Set the read/write timeout threshold (in ms). Defaults to 45000 ms.
                .IsHttps(cosConfig.IsHttps)                                             //Set HTTPS request as default
                .SetAppid(cosConfig.AppId)                                              //Set the Tencent Cloud account ID: APPID
                .SetRegion(cosConfig.Region)                                            //Set a default region of bucket
                .SetDebugLog(cosConfig.DebugLog)                                        //Display logs
                .Build();                                                               //Create a CosXmlConfig object
                var secretId = cosConfig.SecretId;                                      //"Cloud API key SecretId"
                var secretKey = cosConfig.SecretKey;                                   //"Cloud API key SecretKey"
                var durationSecond = cosConfig.DurationSecond;                          //Validity period of secretKey (in sec)
                var cosCredentialProvider = new DefaultQCloudCredentialProvider(secretId, secretKey, durationSecond);
                var cosXmlServer = new CosXmlServer(cosXmlConfig, cosCredentialProvider);
                return cosXmlServer;
            });
            return cosXmlServer;
        }
    }
}
