// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaInfoFactory.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   The media info factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Factories.Apps.MediaInfo
{
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    using DevExpress.XtraEditors;

    using YANFOE.Factories.Apps.MediaInfo.Models;

    /// <summary>
    /// The media info factory.
    /// </summary>
    public static class MediaInfoFactory
    {
        #region Constants and Fields

        /// <summary>
        /// The media info path.
        /// </summary>
        private static string mediaInfoPath =
            Path.Combine(new[] { Application.StartupPath, "Apps", "MediaInfo", "MediaInfo.exe" });

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the mediainfo XML response.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// The get mediainfo xml output
        /// </returns>
        public static string GetMediaInfoXml(string filePath)
        {
            if (!File.Exists(mediaInfoPath))
            {
                XtraMessageBox.Show("MediaInfo.exe not found", filePath, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var mediaInfo = new Process
                {
                    EnableRaisingEvents = false, 
                    StartInfo =
                        {
                            FileName = mediaInfoPath, 
                            Arguments = string.Format(@"""{0}"" --Output=XML", filePath), 
                            UseShellExecute = false, 
                            CreateNoWindow = true, 
                            RedirectStandardOutput = true, 
                            RedirectStandardError = true
                        }
                };

            mediaInfo.Start();
            mediaInfo.WaitForExit();

            return mediaInfo.StandardOutput.ReadToEnd();
        }

        public static MiResponseModel DoMediaInfoScan(string filePath)
        {
            var responseModel = new MiResponseModel();
            var xml = GetMediaInfoXml(filePath);

            responseModel.PopulateFromXML(xml);

            return responseModel;
        }

        #endregion
    }
}