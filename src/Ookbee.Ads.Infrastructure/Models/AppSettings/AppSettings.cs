namespace Ookbee.Ads.Infrastructure.Models
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public ServicesSettings Services { get; set; }
        public TencentSettings Tencent { get; set; }
    }
}
