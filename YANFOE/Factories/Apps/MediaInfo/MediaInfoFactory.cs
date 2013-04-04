// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MediaInfoFactory.cs">
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
//   The media info factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Apps.MediaInfo
{
    #region Required Namespaces

    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows;

    using YANFOE.Factories.Apps.MediaInfo.Models;
    using YANFOE.Models.NFOModels;
    using YANFOE.Settings;
    using YANFOE.Tools.Text;
    using YANFOE.Tools.Xml;

    #endregion

    /// <summary>
    ///   The media info factory.
    /// </summary>
    public static class MediaInfoFactory
    {
        #region Static Fields

        /// <summary>
        ///   The media info path.
        /// </summary>
        private static readonly string MediaInfoPath =
            Path.Combine(new[] { AppDomain.CurrentDomain.BaseDirectory, "Apps", "MediaInfo", "MediaInfo.exe" });

        #endregion

        #region Enums

        /// <summary>
        ///   The scan type.
        /// </summary>
        private enum ScanType
        {
            /// <summary>
            ///   The none.
            /// </summary>
            None, 

            /// <summary>
            ///   The pal.
            /// </summary>
            Pal, 

            /// <summary>
            ///   The NTSC.
            /// </summary>
            Ntsc
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The do media info scan.
        /// </summary>
        /// <param name="filePath">
        /// The file path. 
        /// </param>
        /// <returns>
        /// The <see cref="MiResponseModel"/> . 
        /// </returns>
        public static MiResponseModel DoMediaInfoScan(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            var responseModel = new MiResponseModel();
            var xml = GetMediaInfoXml(filePath);

            responseModel.PopulateFromXml(xml);

            return responseModel;
        }

        /// <summary>
        /// Gets the media info XML response.
        /// </summary>
        /// <param name="filePath">
        /// The file path. 
        /// </param>
        /// <returns>
        /// The get media info xml output 
        /// </returns>
        public static string GetMediaInfoXml(string filePath)
        {
            if (File.Exists(filePath + ".mediainfo"))
            {
                var xml = IO.ReadTextFromFile(filePath + ".mediainfo");
                var check = XTool.IsValidXML(xml);

                if (check)
                {
                    return xml;
                }

                File.Delete(filePath + ".mediainfo");
            }

            if (!File.Exists(MediaInfoPath))
            {
                MessageBox.Show("MediaInfo.exe not found", filePath, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var mediaInfo = new Process
                {
                    EnableRaisingEvents = false, 
                    StartInfo =
                        {
                            FileName = MediaInfoPath, 
                            Arguments = string.Format(@"""{0}"" --Output=XML", filePath), 
                            UseShellExecute = false, 
                            CreateNoWindow = true, 
                            RedirectStandardOutput = true, 
                            RedirectStandardError = true
                        }
                };

            mediaInfo.Start();
            mediaInfo.WaitForExit(Get.MediaInfo.WaitForScan);

            var output = mediaInfo.StandardOutput.ReadToEnd();

            IO.WriteTextToFile(filePath + ".mediainfo", output);

            return output;
        }

        /// <summary>
        /// The inject response model.
        /// </summary>
        /// <param name="responseModel">
        /// The response model. 
        /// </param>
        /// <param name="fileInfoModel">
        /// The file info model. 
        /// </param>
        public static void InjectResponseModel(MiResponseModel responseModel, FileInfoModel fileInfoModel)
        {
            if (responseModel.VideoStreams.Count == 0)
            {
                return;
            }

            fileInfoModel.Codec = responseModel.VideoStreams[0].CodecId;
            fileInfoModel.Width = responseModel.VideoStreams[0].Width;
            fileInfoModel.Height = responseModel.VideoStreams[0].Height;

            fileInfoModel.AspectRatioPercent = responseModel.VideoStreams[0].DisplayAspectRatio.Replace(
                " ", string.Empty);
            fileInfoModel.AspectRatioDecimal = GenerateARDecimal(fileInfoModel.AspectRatioPercent);

            fileInfoModel.FPS = GenerateFps(responseModel.VideoStreams[0].FrameRate);
            fileInfoModel.FPSRounded = GenerateFpsRounded(responseModel.VideoStreams[0].FrameRate);

            fileInfoModel.Ntsc = IsNTSC(fileInfoModel.FPS);
            fileInfoModel.Pal = IsPal(fileInfoModel.FPS);

            fileInfoModel.ProgressiveScan = responseModel.VideoStreams[0].ScanType == "Progressive";
            fileInfoModel.InterlacedScan = responseModel.VideoStreams[0].ScanType == "Interlaced";

            fileInfoModel.SubtitleStreams = responseModel.SubtitleStreams;
            fileInfoModel.AudioStreams = responseModel.AudioStreams;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The generate AR decimal.
        /// </summary>
        /// <param name="arValue">
        /// The AR value. 
        /// </param>
        /// <returns>
        /// The <see cref="string"/> . 
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        private static string GenerateARDecimal(string arValue)
        {
            var regex = Regex.Match(arValue, @"(?<a>.*?):(?<b>\d{1})");

            if (!regex.Success)
            {
                return string.Empty;
            }

            var a = decimal.Parse(regex.Groups["a"].Value);
            var b = decimal.Parse(regex.Groups["b"].Value);

            var value = (a / b).ToString(CultureInfo.InvariantCulture);

            if (value.Length < 5)
            {
                return value;
            }

            return value.Substring(0, 5);
        }

        /// <summary>
        /// The generate fps.
        /// </summary>
        /// <param name="fps">
        /// The fps. 
        /// </param>
        /// <returns>
        /// The <see cref="string"/> . 
        /// </returns>
        private static string GenerateFps(string fps)
        {
            return fps.Replace(" ", string.Empty).Replace("fps", string.Empty);
        }

        /// <summary>
        /// The generate fps rounded.
        /// </summary>
        /// <param name="fps">
        /// The fps. 
        /// </param>
        /// <returns>
        /// The <see cref="string"/> . 
        /// </returns>
        private static string GenerateFpsRounded(string fps)
        {
            fps = GenerateFps(fps);

            decimal result;
            var check = decimal.TryParse(fps, out result);

            if (!check)
            {
                return string.Empty;
            }

            return Math.Round(result).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// The get scan type.
        /// </summary>
        /// <param name="fps">
        /// The fps. 
        /// </param>
        /// <returns>
        /// The <see cref="ScanType"/> . 
        /// </returns>
        private static ScanType GetScanType(string fps)
        {
            decimal result;
            var check = decimal.TryParse(fps, out result);

            if (!check)
            {
                return ScanType.None;
            }

            var rounded = (int)Math.Round(result);

            switch (rounded)
            {
                case 24:
                case 29:
                case 30:
                case 60:
                    return ScanType.Ntsc;
                case 25:
                case 49:
                case 50:
                    return ScanType.Pal;
            }

            return ScanType.None;
        }

        /// <summary>
        /// The is NTSC.
        /// </summary>
        /// <param name="fps">
        /// The fps. 
        /// </param>
        /// <returns>
        /// The <see cref="bool"/> . 
        /// </returns>
        private static bool IsNTSC(string fps)
        {
            return GetScanType(fps) == ScanType.Ntsc;
        }

        /// <summary>
        /// The is pal.
        /// </summary>
        /// <param name="fps">
        /// The fps. 
        /// </param>
        /// <returns>
        /// The <see cref="bool"/> . 
        /// </returns>
        private static bool IsPal(string fps)
        {
            return GetScanType(fps) == ScanType.Pal;
        }

        #endregion
    }
}