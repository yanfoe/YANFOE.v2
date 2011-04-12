// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Binary.cs" company="The YANFOE Project">
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
    using System.Diagnostics;
    using System.IO;
    using System.Net;

    using YANFOE.InternalApps.DownloadManager.Cache;
    using YANFOE.InternalApps.DownloadManager.Model;

    /// <summary>
    /// Download binary to a file.
    /// </summary>
    public class Binary 
    {
        /// <summary>
        /// Gets the specified download item.
        /// </summary>
        /// <param name="downloadItem">The download item.</param>
        public static void Get(DownloadItem downloadItem)
        {
            downloadItem.Result = new BinaryResult();

            if (downloadItem.Url == null || !WebCache.CheckIfUrlIsTooLong(downloadItem.Url))
            {
                return;
            }

            if (!downloadItem.Url.ToLower().StartsWith("http://"))
            {
                downloadItem.Result.Success = true;
                downloadItem.Result.Result = downloadItem.Url.ToLower();
                return;
            }

            var path = WebCache.GetPathFromUrl(downloadItem.Url, downloadItem.Section);

            var pathDir = path.Replace(Path.GetFileName(path), string.Empty);

            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }

            if (!downloadItem.IgnoreCache && File.Exists(path))
            {
                downloadItem.Result.Success = true;
                downloadItem.Result.Result = path;
                return;
            }
            
            if (File.Exists(path))
            {
                //TODO System.IO.IOException: file used by another process - possible exception
                File.Delete(path);
            }

            // the URL to download the file from
            var urlToReadFileFrom = downloadItem.Url;

            // the path to write the file to
            var filePathToWriteFileTo = path;

            downloadItem.Progress.Percent = 0;
            downloadItem.Progress.Message = string.Format("Connecting to {0}", urlToReadFileFrom);

            HttpWebResponse response;
            HttpWebRequest request;
            bool error;
            var count = 0;

            do
            {
                try
                {
                    long runningByteTotal = 0;

                    var url2 = new Uri(urlToReadFileFrom);
                    request = (HttpWebRequest)WebRequest.Create(url2);
                    request.MaximumAutomaticRedirections = 4;
                    request.UserAgent = Settings.Get.Web.UserAgent;
                    request.Timeout = 20000;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    request.ReadWriteTimeout = 20000;
                    request.Proxy = null;
                    request.KeepAlive = false;

                    if (Settings.Get.Web.ProxyUse)
                    {
                        var proxy = new WebProxy(string.Format("{0}:{1}", Settings.Get.Web.ProxyIP, Settings.Get.Web.ProxyPort));

                        if (!string.IsNullOrEmpty(Settings.Get.Web.ProxyUserName) && !string.IsNullOrEmpty(Settings.Get.Web.ProxyPassword))
                        {
                            proxy.Credentials = new NetworkCredential(Settings.Get.Web.ProxyUserName, Settings.Get.Web.ProxyPassword);
                        }

                        request.Proxy = proxy;
                    }
                    else
                    {
                        request.Proxy = null;
                    }

                    request.KeepAlive = false;
                    using (response = (HttpWebResponse)request.GetResponse())
                    {
                        var size = response.ContentLength;

                        var streamRemote = response.GetResponseStream();

                        if (File.Exists(filePathToWriteFileTo) || size == -1)
                        {
                            return;
                        }

                        using (Stream streamLocal = new FileStream(
                            filePathToWriteFileTo,
                            FileMode.Create,
                            FileAccess.Write,
                            FileShare.None))
                        {
                            var byteBuffer = new byte[size];

                            downloadItem.Progress.Percent = 0;
                            downloadItem.Progress.Message = string.Format("Downloading {0}: 0%", urlToReadFileFrom);

                            string speed = "";
                            Stopwatch sw1 = new Stopwatch();

                            if (streamRemote != null)
                            {
                                int byteSize;
                                sw1.Start();
                                while ((byteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                                {
                                    sw1.Stop();
                                    // write the bytes to the file system at the file path specified
                                    streamLocal.Write(byteBuffer, 0, byteSize);
                                    runningByteTotal += byteSize;

                                    // calculate the progress out of a base "100"
                                    var index = (double)runningByteTotal;
                                    var total = (double)byteBuffer.Length;
                                    var progressPercentageDouble = index / total;
                                    var progressPercentageInteger = (int)(progressPercentageDouble * 100);

                                    // calculate the average speed of the download
                                    if (sw1.ElapsedMilliseconds != 0)
                                    {
                                        var speedbps = (double)(((runningByteTotal) / ((double)sw1.ElapsedMilliseconds / 1000)));
                                        var speedkbps = (double)(((runningByteTotal) / ((double)sw1.ElapsedMilliseconds / 1000)) / 1024);
                                        var speedmbps = (double)(((runningByteTotal) / ((double)sw1.ElapsedMilliseconds / 1000)) / (1024 * 1024));
                                        if (speedmbps >= 1) speed = string.Format("{0:#.##} MBytes/s", speedmbps);
                                        else if (speedkbps >= 1) speed = string.Format("{0:#.##} KBytes/s", speedkbps);
                                        else if (speedbps >= 1) speed = string.Format("{0:#.##} Bytes/s", speedbps);
                                        else speed = "";
                                    }

                                    downloadItem.Progress.Percent = progressPercentageInteger;

                                    downloadItem.Progress.Message = string.Format(
                                        "Downloading {1}: {0}% : {2}",
                                        progressPercentageInteger,
                                        urlToReadFileFrom,
                                        speed);

                                    sw1.Start();
                                }
                            }

                            // clean up the file stream
                            streamLocal.Close();
                        }
                    }

                    error = false;
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        if (urlToReadFileFrom.IndexOf("_thumb") > -1)
                        {
                            urlToReadFileFrom = urlToReadFileFrom.Replace("_thumb.jpg", ".jpg");
                        }
                        else
                        {
                            return;
                        }
                    }

                    downloadItem.Progress.Message = string.Format("Restarting Attempt: {0} - {1}", count + 1, urlToReadFileFrom);
                    downloadItem.Progress.Percent = 0;
                    error = true;

                    count++;
                }
            }
            while (error && count < 5);

            if (error)
            {
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch
                {
                    return;
                }

                return;
            }

            downloadItem.Result = new BinaryResult { Success = true, Result = path };

            return;
        }
    }
}