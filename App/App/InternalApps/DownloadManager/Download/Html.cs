// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Html.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0) 
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//   See this page: http://www.yanfoe.com/license
//   For any reuse or distribution, you must make clear to others the 
//   license terms of this work.  
// </license>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.InternalApps.DownloadManager.Download
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.InternalApps.DownloadManager.Cache;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.Tools.Compression;
    using YANFOE.Tools.IO;

    /// <summary>
    /// Download Html.
    /// </summary>
    public class Html
    {
        /// <summary>
        /// Gets the specified download item.
        /// </summary>
        /// <param name="downloadItem">The download item.</param>
        public void Get(DownloadItem downloadItem)
        {
            Log.WriteToLog(LogSeverity.Info, downloadItem.ThreadID, string.Format("Processing HTML Started"), string.Format("{0}", downloadItem.Url));

            downloadItem.Result = new HtmlResult();

            downloadItem.Progress.Message = "Downloading " + downloadItem.Url;

            var cachePath = WebCache.GetPathFromUrl(downloadItem.Url, downloadItem.Section);

            Folders.CheckExists(cachePath, true);

            if (!downloadItem.IgnoreCache && !downloadItem.Url.Contains("YNoCache"))
            {
                if (File.Exists(cachePath + ".txt.gz"))
                {
                    Log.WriteToLog(LogSeverity.Info, downloadItem.ThreadID, string.Format(CultureInfo.CurrentCulture, "Url Found in Cache"), string.Format(CultureInfo.CurrentCulture, "{0}", downloadItem.Url));
                    downloadItem.Result = new HtmlResult
                        {
                            Result = Gzip.Decompress(cachePath + ".txt.gz"), 
                            Success = true
                        };

                    return;
                }
            }

            if (InternalHandlers.Check(downloadItem))
            {
                InternalHandlers.Process(downloadItem, cachePath);
                return;
            }

            try
            {
                Log.WriteToLog(
                    LogSeverity.Info,
                    downloadItem.ThreadID,
                    string.Format(CultureInfo.CurrentCulture, "Downloading"),
                    string.Format(CultureInfo.CurrentCulture, "{0}", downloadItem.Url));
                
                downloadItem.Url = downloadItem.Url.Replace(" ", "+").Replace("%20", "+");

                var webClient = new WebClient
                    {
                        Proxy = null
                    };

                if (Settings.Get.Web.EnableProxy)
                {
                    webClient.Proxy = new WebProxy(Settings.Get.Web.ProxyUserName, (int)Settings.Get.Web.ProxyPort);
                }

                webClient.Headers.Add("user-agent", Settings.Get.Web.UserAgent);

                var encode = Encoding.GetEncoding(1252);

                foreach (var encoding in Settings.Get.Web.WebEncodings)
                {
                    if (downloadItem.Url.Contains(encoding.Key))
                    {
                        encode = encoding.Value;
                        break;
                    }
                }

                webClient.Encoding = encode;

                var outputString = webClient.DownloadString(downloadItem.Url);

                Log.WriteToLog(
                    LogSeverity.Info,
                    downloadItem.ThreadID,
                    string.Format(CultureInfo.CurrentCulture, "Download Complete. Saving to Cache"),
                    string.Format(CultureInfo.CurrentCulture, "{0}", downloadItem.Url));

                if (encode != Encoding.UTF8)
                {
                    var origBytes = encode.GetBytes(outputString);
                    var newBytes = Encoding.Convert(encode, Encoding.UTF8, origBytes);
                    outputString = Encoding.UTF8.GetString(newBytes);
                    encode = Encoding.UTF8;
                }

                File.WriteAllText(cachePath + ".txt.tmp", outputString, encode);

                Gzip.Compress(cachePath + ".txt.tmp", cachePath + ".txt.gz");
                File.Delete(cachePath + ".txt.tmp");

                Log.WriteToLog(
                    LogSeverity.Info,
                    downloadItem.ThreadID,
                    string.Format(CultureInfo.CurrentCulture, "Process Complete."),
                    string.Format(CultureInfo.CurrentCulture, "{0}", downloadItem.Url));


                downloadItem.Result.Result = outputString;
                downloadItem.Result.Success = true;
                return;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, string.Format("Download Html {0}", downloadItem.Url), ex.Message);
                return;
            }
        }
    }
}