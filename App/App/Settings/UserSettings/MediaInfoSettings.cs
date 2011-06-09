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
    using System.Collections.Generic;
    using System.Drawing;

    public class MediaInfoSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaInfoSettings"/> class.
        /// </summary>
        public MediaInfoSettings()
        {
            this.WaitForScan = 3000;

            this.Resolutions = new Dictionary<string, Size>(3);

            this.Resolutions.Add("480", new Size(1, 799));
            this.Resolutions.Add("576", new Size(0, 0));
            this.Resolutions.Add("720", new Size(800, 1280));
            this.Resolutions.Add("1080", new Size(1281, 1920));

            this.VideoOutput480 = "[P] [D][S]";
            this.VideoOutput720 = "[[R][S] [D]Hz]";
            this.VideoOutput1080 = "[R][S] [D]Hz";

            this.AspectRatioString = "[E]";
        }

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
    }
}
