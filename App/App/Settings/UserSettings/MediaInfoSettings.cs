// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaInfoSettings.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the MediaInfoSettings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Settings.UserSettings
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using YANFOE.Models.NFOModels;

    public class MediaInfoSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaInfoSettings"/> class.
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

        public string DoReplace(string value, FileInfoModel fileInfoModel)
        {
            value = value.Replace("%R", fileInfoModel.Resolution);
            value = value.Replace("%F", fileInfoModel.FPS);
            value = value.Replace("%D", fileInfoModel.FPSRounded);
            value = value.Replace("%S", fileInfoModel.ScanType);
            value = value.Replace("%P", fileInfoModel.VideoType);

            return value.Trim();
        }

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

        public string KeyResolution { get; private set; }

        public string KeyFPS { get; private set; }

        public string KeyScanType { get; private set; }

        public string KeyRoundedFPS { get; private set; }

        public string KeyNTSCPal { get; private set; }

        /// <summary>
        /// Gets or sets Resolution480From.
        /// </summary>
        public int Resolution480From { get; set; }

        /// <summary>
        /// Gets or sets Resolution480To.
        /// </summary>
        public int Resolution480To { get; set; }

        /// <summary>
        /// Gets or sets Resolution576From.
        /// </summary>
        public int Resolution576From { get; set; }

        /// <summary>
        /// Gets or sets Resolution576To.
        /// </summary>
        public int Resolution576To { get; set; }

        public int Resolution720From { get; set; }

        public int Resolution720To { get; set; }

        public int Resolution1080From { get; set; }

        public int Resolution1080To { get; set; }

        public int WaitForScan { get; set; }

        public string[] AspectRatio = new[]
            {
                "16:9",
                "4:3",
                "3:2",
                "1.85:1",
                "2.39:1",
                "1.85:1",
                "2.39:1",
                "1.33:1",
                "1.37:1",
                "1.43:1",
                "1.50:1",
                "1.56:1",
                "1.66:1",
                "1.75:1",
                "1.78:1",
                "1.85:1",
                "2.00:1",
                "2.20:1",
                "2.35:1",
                "2.39:1",
                "2.55:1",
                "2.59:1",
                "2.66:1",
                "2.76:1",
                "4.00:1"
            };

        public string[] AspectRatioDecimal = new[]
            {
                "1.777", "1.333", "1.5", "1.85", "2.39", "1.33", "1.37", "1.43", "1.50", "1.56", "1.66", "1.75", "1.78",
                "1.85", "2.00", "2.20", "2.35", "2.39", "2.55", "2.59", "2.66", "2.76", "4.00"
            };

        public Dictionary<string, Size> Resolutions { get; set; }

        public string AspectRatioString { get; set; }

        public bool UseDARInsteadOfPAR { get; set; }

        public string VideoOutput480 { get; set; }

        public string VideoOutput720 { get; set; }

        public string VideoOutput1080 { get; set; }

        public bool OutputPWhenFIs24 { get; set; }

        public bool UseDecimalAspectRatio { get; set; }

        public bool UsePercentAspectRatio { get; set; }

        public bool ScanNTSC { get; set; }

        public bool ScanPAL { get; set; }
    }
}
