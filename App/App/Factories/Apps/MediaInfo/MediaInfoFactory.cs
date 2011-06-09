﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaInfoFactory.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   The media info factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Factories.Apps.MediaInfo
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    using DevExpress.XtraEditors;

    using YANFOE.Factories.Apps.MediaInfo.Models;
    using YANFOE.Models.MovieModels;
    using YANFOE.Tools.Xml;

    /// <summary>
    /// The media info factory.
    /// </summary>
    public static class MediaInfoFactory
    {
        #region Constants and Fields

        /// <summary>
        /// The media info path.
        /// </summary>
        private static readonly string mediaInfoPath =
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
            if (File.Exists(filePath + ".mediainfo"))
            {
                var xml = Tools.Text.IO.ReadTextFromFile(filePath + ".mediainfo");
                var check = XTool.IsValidXML(xml);

                if (check)
                {
                    return xml;
                }
                
                File.Delete(filePath + ".mediainfo");
            }

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
            mediaInfo.WaitForExit(Settings.Get.MediaInfo.WaitForScan);

            var output = mediaInfo.StandardOutput.ReadToEnd();

            Tools.Text.IO.WriteTextToFile(filePath + ".mediainfo", output);

            return output;
        }

        public static MiResponseModel DoMediaInfoScan(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            var responseModel = new MiResponseModel();
            var xml = GetMediaInfoXml(filePath);

            responseModel.PopulateFromXML(xml);

            return responseModel;
        }

        public static void InjectResponseModel(MiResponseModel responseModel, MovieModel movieModel)
        {
            if (responseModel.VideoStreams.Count == 0)
            {
                return;
            }

            movieModel.FileInfo.Codec = responseModel.VideoStreams[0].CodecID;
            movieModel.FileInfo.Width = responseModel.VideoStreams[0].Width;
            movieModel.FileInfo.Height = responseModel.VideoStreams[0].Height;

            movieModel.FileInfo.AspectRatio = responseModel.VideoStreams[0].DisplayAspectRatio.Replace(" ", string.Empty);
            movieModel.FileInfo.AspectRatioDecimal = GenerateARDecimal(movieModel.FileInfo.AspectRatio);

            movieModel.FileInfo.FPS = GenerateFPS(responseModel.VideoStreams[0].FrameRate);
            movieModel.FileInfo.FPSRounded = GenerateFPSRounded(responseModel.VideoStreams[0].FrameRate);

            movieModel.FileInfo.Ntsc = IsNtsc(movieModel.FileInfo.FPS);
            movieModel.FileInfo.Pal = IsPal(movieModel.FileInfo.FPS);

            movieModel.FileInfo.Resolution = GenerateResolution(movieModel.FileInfo.Width);

            movieModel.FileInfo.ProgressiveScan = responseModel.VideoStreams[0].ScanType == "Progressive";
            movieModel.FileInfo.InterlacedScan = responseModel.VideoStreams[0].ScanType == "Interlaced";
        }

        private static string GenerateFPS(string fps)
        {
            return fps.Replace(" ", string.Empty).Replace("fps", string.Empty);
        }

        private static string GenerateFPSRounded(string fps)
        {
            fps = GenerateFPS(fps);

            decimal result;
            var check = decimal.TryParse(fps, out result);

            if (!check)
            {
                return string.Empty;
            }

            return Math.Round(result).ToString();
        }

        private static string GenerateResolution(int width)
        {
            foreach (var resolution in Settings.Get.MediaInfo.Resolutions)
            {
                if (resolution.Value.Width <= width && resolution.Value.Height >= width)
                {
                    return resolution.Key;
                }
            }

            return string.Empty;
        }

        private static string GenerateARDecimal(string arValue)
        {
            var regex = Regex.Match(arValue, @"(?<a>.*?):(?<b>\d{1})");

            if (!regex.Success)
            {
                return string.Empty;
            }

            var a = decimal.Parse(regex.Groups["a"].Value);
            var b = decimal.Parse(regex.Groups["b"].Value);

            var value = (a / b).ToString();

            if (value.Length < 5)
            {
                return value;
            }

            return value.Substring(0, 5);
        }

        enum ScanType{
            None, 
            Pal,
            Ntsc
        }

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

        private static bool IsNtsc(string fps)
        {
            return (GetScanType(fps) == ScanType.Ntsc);
        }

        private static bool IsPal(string fps)
        {
            return (GetScanType(fps) == ScanType.Pal);
        }
    }

        #endregion
}
