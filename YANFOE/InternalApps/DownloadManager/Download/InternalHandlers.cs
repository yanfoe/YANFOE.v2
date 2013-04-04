namespace YANFOE.InternalApps.DownloadManager.Download
{
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Tools.Compression;

    public static class InternalHandlers
    {
        private const string MatchRegex = @"\$\$Internal_(?<internalType>.*?)\?(?<idType>.*?)=(?<id>.*?)$";

        public static bool Check(DownloadItem downloadItem)
        {
            return Regex.IsMatch(downloadItem.Url, MatchRegex);
        }

        public static string Process(DownloadItem downloadItem, string cachePath)
        {
            var regexMatch = Regex.Match(downloadItem.Url, MatchRegex);

            var internalType = regexMatch.Groups["internalType"].Value;
            var idType = regexMatch.Groups["idType"].Value;
            var id = regexMatch.Groups["id"].Value;
            string output = string.Empty;

            switch (internalType)
            {
                case "movieMeterHandler":
                    output = Tools.ThirdParty.MovieMeterApiHandler.GenerateMovieMeterXml(downloadItem, idType, id);
                    break;
            }

            if (!string.IsNullOrEmpty(output))
            {
                File.WriteAllText(cachePath + ".txt.tmp",output, Encoding.UTF8);
                Gzip.Compress(cachePath + ".txt.tmp", cachePath + ".txt.gz");
                File.Delete(cachePath + ".txt.tmp");
            }

            return output;
        }
    }
}
