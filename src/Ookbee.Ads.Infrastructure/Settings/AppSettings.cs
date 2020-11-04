namespace Ookbee.Ads.Infrastructure.Settings
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public CronJobsSettings CronJobs { get; set; }
        public ServicesSettings Services { get; set; }
        public TencentSettings Tencent { get; set; }
    }
}
