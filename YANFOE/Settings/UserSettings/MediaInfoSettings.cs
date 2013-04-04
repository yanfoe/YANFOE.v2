// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MediaInfoSettings.cs">
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
//   The media info settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Settings.UserSettings
{
    #region Required Namespaces

    using System.Collections.Generic;
    using System.Drawing;

    using YANFOE.Models.NFOModels;

    #endregion

    /// <summary>
    /// The media info settings.
    /// </summary>
    public class MediaInfoSettings
    {
        #region Fields

        /// <summary>
        /// The aspect ratio.
        /// </summary>
        public string[] AspectRatio = new[]
            {
                "16:9", "4:3", "3:2", "1.85:1", "2.39:1", "1.85:1", "2.39:1", "1.33:1", "1.37:1", "1.43:1", "1.50:1", 
                "1.56:1", "1.66:1", "1.75:1", "1.78:1", "1.85:1", "2.00:1", "2.20:1", "2.35:1", "2.39:1", "2.55:1", 
                "2.59:1", "2.66:1", "2.76:1", "4.00:1"
            };

        /// <summary>
        /// The aspect ratio decimal.
        /// </summary>
        public string[] AspectRatioDecimal = new[]
            {
                "1.777", "1.333", "1.5", "1.85", "2.39", "1.33", "1.37", "1.43", "1.50", "1.56", "1.66", "1.75", "1.78", 
                "1.85", "2.00", "2.20", "2.35", "2.39", "2.55", "2.59", "2.66", "2.76", "4.00"
            };

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="MediaInfoSettings" /> class.
        /// </summary>
        public MediaInfoSettings()
        {
            this.WaitForScan = 3000;

            this.Resolution480From = 1;
            this.Resolution480To = 799;

            this.Resolution576From = 0;
            this.Resolution576To = 0;

            this.Resolution720From = 800;
            this.Resolution720To = 1280;

            this.Resolution1080From = 1281;
            this.Resolution1080To = 1920;

            this.VideoOutput480 = "%P %D%S";
            this.VideoOutput720 = "%R%S %DHz";
            this.VideoOutput1080 = "%R%S %DHz";

            this.AspectRatioString = "%E";

            this.KeyResolution = "%R";
            this.KeyFPS = "%F";
            this.KeyRoundedFPS = "%D";
            this.KeyScanType = "%S";
            this.KeyNTSCPal = "%P";

            this.UsePercentAspectRatio = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the aspect ratio string.
        /// </summary>
        public string AspectRatioString { get; set; }

        /// <summary>
        /// Gets the key fps.
        /// </summary>
        public string KeyFPS { get; private set; }

        /// <summary>
        /// Gets the key ntsc pal.
        /// </summary>
        public string KeyNTSCPal { get; private set; }

        /// <summary>
        /// Gets the key resolution.
        /// </summary>
        public string KeyResolution { get; private set; }

        /// <summary>
        /// Gets the key rounded fps.
        /// </summary>
        public string KeyRoundedFPS { get; private set; }

        /// <summary>
        /// Gets the key scan type.
        /// </summary>
        public string KeyScanType { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether output p when f is 24.
        /// </summary>
        public bool OutputPWhenFIs24 { get; set; }

        /// <summary>
        /// Gets or sets the resolution 1080 from.
        /// </summary>
        public int Resolution1080From { get; set; }

        /// <summary>
        /// Gets or sets the resolution 1080 to.
        /// </summary>
        public int Resolution1080To { get; set; }

        /// <summary>
        ///   Gets or sets Resolution480From.
        /// </summary>
        public int Resolution480From { get; set; }

        /// <summary>
        ///   Gets or sets Resolution480To.
        /// </summary>
        public int Resolution480To { get; set; }

        /// <summary>
        ///   Gets or sets Resolution576From.
        /// </summary>
        public int Resolution576From { get; set; }

        /// <summary>
        ///   Gets or sets Resolution576To.
        /// </summary>
        public int Resolution576To { get; set; }

        /// <summary>
        /// Gets or sets the resolution 720 from.
        /// </summary>
        public int Resolution720From { get; set; }

        /// <summary>
        /// Gets or sets the resolution 720 to.
        /// </summary>
        public int Resolution720To { get; set; }

        /// <summary>
        /// Gets or sets the resolutions.
        /// </summary>
        public Dictionary<string, Size> Resolutions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether scan ntsc.
        /// </summary>
        public bool ScanNTSC { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether scan pal.
        /// </summary>
        public bool ScanPAL { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use dar instead of par.
        /// </summary>
        public bool UseDARInsteadOfPAR { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use decimal aspect ratio.
        /// </summary>
        public bool UseDecimalAspectRatio { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use percent aspect ratio.
        /// </summary>
        public bool UsePercentAspectRatio { get; set; }

        /// <summary>
        /// Gets or sets the video output 1080.
        /// </summary>
        public string VideoOutput1080 { get; set; }

        /// <summary>
        /// Gets or sets the video output 480.
        /// </summary>
        public string VideoOutput480 { get; set; }

        /// <summary>
        /// Gets or sets the video output 720.
        /// </summary>
        public string VideoOutput720 { get; set; }

        /// <summary>
        /// Gets or sets the wait for scan.
        /// </summary>
        public int WaitForScan { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The do replace.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="fileInfoModel">
        /// The file info model.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string DoReplace(string value, FileInfoModel fileInfoModel)
        {
            value = value.Replace("%R", fileInfoModel.Resolution);
            value = value.Replace("%F", fileInfoModel.FPS);
            value = value.Replace("%D", fileInfoModel.FPSRounded);
            value = value.Replace("%S", fileInfoModel.ScanType);
            value = value.Replace("%P", fileInfoModel.VideoType);

            return value.Trim();
        }

        /// <summary>
        /// The do replace.
        /// </summary>
        /// <param name="fileInfoModel">
        /// The file info model.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string DoReplace(FileInfoModel fileInfoModel)
        {
            if (fileInfoModel.Resolution == "1080")
            {
                return this.DoReplace(this.VideoOutput1080, fileInfoModel);
            }

            if (fileInfoModel.Resolution == "720")
            {
                return this.DoReplace(this.VideoOutput720, fileInfoModel);
            }

            if (fileInfoModel.Resolution == "480")
            {
                return this.DoReplace(this.VideoOutput480, fileInfoModel);
            }

            return string.Empty;
        }

        /// <summary>
        /// The do replace demo.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string DoReplaceDemo(string value, int width, int height)
        {
            var fileInfoModel = new FileInfoModel
                {
                    Height = height, 
                    Width = width, 
                    Codec = "V_MPEG4/ISO/AVC", 
                    FPS = "25", 
                    FPSRounded = "25", 
                    Ntsc = true, 
                    ProgressiveScan = true
                };

            return this.DoReplace(value, fileInfoModel);
        }

        #endregion
    }
}