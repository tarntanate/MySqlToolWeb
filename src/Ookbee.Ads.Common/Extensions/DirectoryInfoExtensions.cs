using System.IO;
using System.Linq;

namespace Ookbee.Ads.Common.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static FileInfo GetFile(this DirectoryInfo directoryInfo, string name)
        {
            return directoryInfo.GetFiles(string.Concat(name, ".", "*")).Single();
        }
    }
}
