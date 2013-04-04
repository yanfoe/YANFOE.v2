// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MediaScanOutputModel.cs">
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
//   The media scan output.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.GeneralModels.AssociatedFiles
{
    #region Required Namespaces

    using System;
    using System.Globalization;
    using System.Xml;

    using YANFOE.Tools.Models;
    using YANFOE.Tools.UI;
    using YANFOE.Tools.Xml;

    #endregion

    /// <summary>
    ///   The media scan output.
    /// </summary>
    [Serializable]
    public class MediaScanOutput : ModelBase
    {
        #region Fields

        /// <summary>
        ///   The media info output.
        /// </summary>
        private string mediaInfoOutput;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="MediaScanOutput" /> class.
        /// </summary>
        public MediaScanOutput()
        {
            this.AudioStreams = new ThreadedBindingList<AudioStreamModel>();
            this.Subtitles = new ThreadedBindingList<SubtitleStreamModel>();

            this.Format = string.Empty;
            this.FileSize = string.Empty;
            this.Duration = string.Empty;
            this.OverallBitRate = string.Empty;
            this.EncodedDate = string.Empty;
            this.WritingApplication = string.Empty;
            this.WritingLibrary = string.Empty;

            this.VideoID = string.Empty;
            this.VideoFormat = string.Empty;
            this.VideoFormatProfile = string.Empty;
            this.VideoCodecID = string.Empty;
            this.VideoCodecIDHint = string.Empty;
            this.VideoDuration = string.Empty;
            this.VideoBitRate = string.Empty;
            this.VideoWidth = string.Empty;
            this.VideoHeight = string.Empty;
            this.VideoDisplayAspectRatio = string.Empty;
            this.VideoFrame = string.Empty;
            this.VideoResolution = string.Empty;
            this.VideoColorimetry = string.Empty;
            this.VideoScanType = string.Empty;
            this.VideoBits = string.Empty;
            this.VideoStreamSize = string.Empty;
            this.VideoWritingLibrary = string.Empty;

            this.mediaInfoOutput = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets AudioStreams.
        /// </summary>
        public ThreadedBindingList<AudioStreamModel> AudioStreams { get; set; }

        /// <summary>
        ///   Gets or sets Duration.
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        ///   Gets or sets EncodedDate.
        /// </summary>
        public string EncodedDate { get; set; }

        /// <summary>
        ///   Gets or sets FilePath.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        ///   Gets or sets FileSize.
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        ///   Gets or sets Format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        ///   Gets or sets MediaInfoOutput.
        /// </summary>
        public string MediaInfoOutput
        {
            get
            {
                return this.mediaInfoOutput;
            }

            set
            {
                if (value == string.Empty)
                {
                    this.mediaInfoOutput = value;
                    return;
                }

                this.mediaInfoOutput = XTool.SanitizeXmlString(value);
                this.mediaInfoOutput = this.mediaInfoOutput.Replace("&", string.Empty);

                var doc = new XmlDocument();
                doc.LoadXml(this.mediaInfoOutput);

                XmlNodeList track = doc.GetElementsByTagName("track");

                foreach (XmlNode result in track)
                {
                    if (result.Attributes != null && result.Attributes["type"].Value == "Video")
                    {
                        foreach (XmlNode subresults in result.ChildNodes)
                        {
                            switch (subresults.Name)
                            {
                                case "Format":
                                    this.VideoFormat = subresults.InnerText;
                                    break;
                                case "Format_profile":
                                    this.VideoFormatProfile = subresults.InnerText;
                                    break;
                                case "Codec_ID":
                                    this.VideoCodecID = subresults.InnerText;
                                    break;
                                case "Codec_ID_Hint":
                                    this.VideoCodecIDHint = subresults.InnerText;
                                    break;
                                case "Duration":
                                    this.VideoDuration = subresults.InnerText;
                                    break;
                                case "Bit_rate":
                                    this.VideoBitRate = subresults.InnerText;
                                    break;
                                case "Width":
                                    this.VideoWidth = subresults.InnerText;
                                    break;
                                case "Height":
                                    this.VideoHeight = subresults.InnerText;
                                    break;
                                case "Display_aspect_ratio":
                                    this.VideoDisplayAspectRatio = subresults.InnerText;
                                    break;
                                case "Frame_rate":
                                    this.VideoFrame = subresults.InnerText;
                                    break;
                                case "Resolution":
                                    this.VideoResolution = subresults.InnerText;
                                    break;
                                case "Colorimetry":
                                    this.VideoColorimetry = subresults.InnerText;
                                    break;
                                case "Scan_type":
                                    this.VideoScanType = subresults.InnerText;
                                    break;
                                case "Stream_size":
                                    this.VideoStreamSize = subresults.InnerText;
                                    break;
                                case "Writing_library":
                                    this.VideoWritingLibrary = subresults.InnerText;
                                    break;
                            }
                        }
                    }
                    else if (result.Attributes != null && result.Attributes["type"].Value == "Audio")
                    {
                        var stream = new AudioStreamModel();
                        try
                        {
                            stream.ID = int.Parse(result.Attributes["streamid"].InnerText, CultureInfo.CurrentCulture);
                        }
                        catch
                        {
                            stream.ID = 1;
                        }

                        foreach (XmlNode subresults in result.ChildNodes)
                        {
                            switch (subresults.Name)
                            {
                                case "Format":
                                    stream.Format = subresults.InnerText.Replace("AC-3", "AC3");
                                    break;
                                case "Format_Info":
                                    stream.Format_Info = subresults.InnerText;
                                    break;
                                case "Format_profile":
                                    stream.FormatProfile = subresults.InnerText;
                                    break;
                                case "Codec_ID":
                                    stream.CodecID = subresults.InnerText;
                                    break;
                                case "Codec_ID_Hint":
                                    stream.Format = subresults.InnerText;
                                    break;
                                case "Duration":
                                    stream.Duration = subresults.InnerText;
                                    break;
                                case "Bit_rate_mode":
                                    stream.BitRateMode = subresults.InnerText;
                                    break;
                                case "Bit_rate":
                                    stream.Bitrate = subresults.InnerText;
                                    break;
                                case "Channel_s_":
                                    stream.Channels =
                                        subresults.InnerText.Replace(" channels", string.Empty).Replace(
                                            " channel", string.Empty);
                                    break;
                                case "Channel_positions":
                                    stream.ChannelPositions = subresults.InnerText;
                                    break;
                                case "Sampling_rate":
                                    stream.SamplingRate = subresults.InnerText;
                                    break;
                                case "Video_delay":
                                    stream.VideoDelay = subresults.InnerText;
                                    break;
                                case "Stream_size":
                                    stream.StreamSize = subresults.InnerText;
                                    break;
                                case "Language":
                                    stream.Language = subresults.InnerText;
                                    break;
                            }
                        }

                        if (stream.FormatProfile.Contains("TrueHD"))
                        {
                            stream.Format = "TrueHD";
                        }
                        else if ((stream.Format == "DTS") && (stream.FormatProfile == "MA"))
                        {
                            stream.Format = "DTS-HD";
                        }
                        else if ((stream.Format == "DTS") && (stream.FormatProfile == "HRA"))
                        {
                            stream.Format = "DTS-HD";
                        }

                        this.AudioStreams.Add(stream);
                    }
                    else if (result.Attributes != null && result.Attributes["type"].Value == "Text")
                    {
                        var stream = new SubtitleStreamModel();

                        try
                        {
                            stream.ID = int.Parse(result.Attributes["streamid"].InnerText, CultureInfo.CurrentCulture);
                        }
                        catch
                        {
                            stream.ID = 1;
                        }

                        foreach (XmlNode subresults in result.ChildNodes)
                        {
                            string sub;
                            switch (subresults.Name)
                            {
                                case "Format":
                                    sub = subresults.InnerText;
                                    if (sub.Contains("/"))
                                    {
                                        string[] subtitles = sub.Split('/');

                                        sub = subtitles[0].Trim();
                                    }

                                    if (sub.ToLower(CultureInfo.CurrentCulture) == "rle")
                                    {
                                        sub = string.Empty;
                                    }

                                    stream.Format = sub;
                                    break;
                                case "Codec_ID":
                                    stream.Format = subresults.InnerText;
                                    break;
                                case "Codec_ID_Hint":
                                    stream.Format = subresults.InnerText;
                                    break;
                                case "Codec_ID_Info":
                                    stream.Format = subresults.InnerText;
                                    break;
                                case "Language":
                                    sub = subresults.InnerText;
                                    if (sub.Contains("/"))
                                    {
                                        string[] subtitles = sub.Split('/');
                                        sub = subtitles[0].Trim();
                                    }

                                    if (sub.ToLower(CultureInfo.CurrentCulture) == "rle")
                                    {
                                        sub = string.Empty;
                                    }

                                    stream.Format = sub;
                                    break;
                            }
                        }

                        this.Subtitles.Add(stream);
                    }
                }
            }
        }

        /// <summary>
        ///   Gets or sets OverallBitRate.
        /// </summary>
        public string OverallBitRate { get; set; }

        /// <summary>
        ///   Gets or sets Subtitles.
        /// </summary>
        public ThreadedBindingList<SubtitleStreamModel> Subtitles { get; set; }

        /// <summary>
        ///   Gets or sets VideoBitRate.
        /// </summary>
        public string VideoBitRate { get; set; }

        /// <summary>
        ///   Gets or sets VideoBits.
        /// </summary>
        public string VideoBits { get; set; }

        /// <summary>
        ///   Gets or sets VideoCodecID.
        /// </summary>
        public string VideoCodecID { get; set; }

        /// <summary>
        ///   Gets or sets VideoCodecIDHint.
        /// </summary>
        public string VideoCodecIDHint { get; set; }

        /// <summary>
        ///   Gets or sets Video Colorimetry.
        /// </summary>
        public string VideoColorimetry { get; set; }

        /// <summary>
        ///   Gets or sets VideoDisplayAspectRatio.
        /// </summary>
        public string VideoDisplayAspectRatio { get; set; }

        /// <summary>
        ///   Gets or sets VideoDuration.
        /// </summary>
        public string VideoDuration { get; set; }

        /// <summary>
        ///   Gets or sets VideoFormat.
        /// </summary>
        public string VideoFormat { get; set; }

        /// <summary>
        ///   Gets or sets VideoFormatProfile.
        /// </summary>
        public string VideoFormatProfile { get; set; }

        /// <summary>
        ///   Gets or sets VideoFrame.
        /// </summary>
        public string VideoFrame { get; set; }

        /// <summary>
        ///   Gets or sets VideoHeight.
        /// </summary>
        public string VideoHeight { get; set; }

        /// <summary>
        ///   Gets or sets VideoID.
        /// </summary>
        public string VideoID { get; set; }

        /// <summary>
        ///   Gets or sets VideoResolution.
        /// </summary>
        public string VideoResolution { get; set; }

        /// <summary>
        ///   Gets or sets VideoScanType.
        /// </summary>
        public string VideoScanType { get; set; }

        /// <summary>
        ///   Gets or sets VideoStreamSize.
        /// </summary>
        public string VideoStreamSize { get; set; }

        /// <summary>
        ///   Gets or sets VideoWidth.
        /// </summary>
        public string VideoWidth { get; set; }

        /// <summary>
        ///   Gets or sets VideoWritingLibrary.
        /// </summary>
        public string VideoWritingLibrary { get; set; }

        /// <summary>
        ///   Gets or sets WritingApplication.
        /// </summary>
        public string WritingApplication { get; set; }

        /// <summary>
        ///   Gets or sets WritingLibrary.
        /// </summary>
        public string WritingLibrary { get; set; }

        #endregion
    }
}