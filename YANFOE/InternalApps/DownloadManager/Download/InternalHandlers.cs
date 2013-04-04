// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="InternalHandlers.cs">
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
// <summary>
//   The internal handlers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.InternalApps.DownloadManager.Download
{
    #region Required Namespaces

    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Tools.Compression;
    using YANFOE.Tools.ThirdParty;

    #endregion

    /// <summary>
    ///   The internal handlers.
    /// </summary>
    public static class InternalHandlers
    {
        #region Constants

        /// <summary>
        ///   The match regex.
        /// </summary>
        private const string MatchRegex = @"\$\$Internal_(?<internalType>.*?)\?(?<idType>.*?)=(?<id>.*?)$";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The check.
        /// </summary>
        /// <param name="downloadItem">
        /// The download item. 
        /// </param>
        /// <returns>
        /// The <see cref="bool"/> . 
        /// </returns>
        public static bool Check(DownloadItem downloadItem)
        {
            return Regex.IsMatch(downloadItem.Url, MatchRegex);
        }

        /// <summary>
        /// The process.
        /// </summary>
        /// <param name="downloadItem">
        /// The download item. 
        /// </param>
        /// <param name="cachePath">
        /// The cache path. 
        /// </param>
        /// <returns>
        /// The <see cref="string"/> . 
        /// </returns>
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
                    output = MovieMeterApiHandler.GenerateMovieMeterXml(downloadItem, idType, id);
                    break;
            }

            if (!string.IsNullOrEmpty(output))
            {
                File.WriteAllText(cachePath + ".txt.tmp", output, Encoding.UTF8);
                Gzip.Compress(cachePath + ".txt.tmp", cachePath + ".txt.gz");
                File.Delete(cachePath + ".txt.tmp");
            }

            return output;
        }

        #endregion
    }
}