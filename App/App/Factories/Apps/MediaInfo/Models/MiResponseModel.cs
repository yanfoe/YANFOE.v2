// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiResponseModel.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   The mi response model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Factories.Apps.MediaInfo.Models
{
    using System;
    using System.ComponentModel;
    using System.Xml;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Tools.Extentions;

    /// <summary>
    /// The mi response model.
    /// </summary>
    [Serializable]
    public class MiResponseModel
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "MiResponseModel" /> class.
        /// </summary>
        public MiResponseModel()
        {
            this.UniqueID = string.Empty;
            this.CompleteName = string.Empty;
            this.Format = string.Empty;
            this.FileSize = string.Empty;
            this.Duration = string.Empty;
            this.OverallBitRate = string.Empty;
            this.EncodedDate = string.Empty;
            this.EncodedApplication = string.Empty;
            this.EncodedLibrary = string.Empty;
            this.WritingApplication = string.Empty;
            this.WritingLibrary = string.Empty;

            this.VideoStreams = new BindingList<MiVideoStreamModel>();
            this.AudioStreams = new BindingList<MiAudioStreamModel>();
            this.SubtitleStreams = new BindingList<MiSubtitleStreamModel>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the scan XML.
        /// </summary>
        public string ScanXML
        {
            get
            {
                return Tools.Text.IO.ReadTextFromFile(this.CompleteName + ".mediainfo");
            }
        }

        /// <summary>
        ///   Gets or sets AudioStreams.
        /// </summary>
        public BindingList<MiAudioStreamModel> AudioStreams { get; set; }

        /// <summary>
        ///   Gets or sets the name of the complete.
        /// </summary>
        public string CompleteName { get; set; }

        /// <summary>
        ///   Gets or sets the duration.
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        ///   Gets or sets the encoded application.
        /// </summary>
        public string EncodedApplication { get; set; }

        /// <summary>
        ///   Gets or sets the encoded date.
        /// </summary>
        public string EncodedDate { get; set; }

        /// <summary>
        ///   Gets or sets the encoded library.
        /// </summary>
        public string EncodedLibrary { get; set; }

        /// <summary>
        /// Gets or sets the writing application.
        /// </summary>
        public string WritingApplication { get; set; }

        /// <summary>
        /// Gets or sets the writing library.
        /// </summary>
        public string WritingLibrary { get; set; }

        /// <summary>
        ///   Gets or sets the full size string.
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        ///   Gets or sets the format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        ///   Gets or sets the overall bit rate.
        /// </summary>
        public string OverallBitRate { get; set; }

        /// <summary>
        ///   Gets or sets the subtitle streams.
        /// </summary>
        public BindingList<MiSubtitleStreamModel> SubtitleStreams { get; set; }

        /// <summary>
        ///   Gets or sets the unique ID.
        /// </summary>
        public string UniqueID { get; set; }

        /// <summary>
        ///   Gets or sets the subtitle streams.
        /// </summary>
        public BindingList<MiVideoStreamModel> VideoStreams { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Populates all values based on inputted xml.
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        /// <returns>
        /// Succeeded without error
        /// </returns>
        public bool PopulateFromXML(string xml)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                var track = doc.GetElementsByTagName("track");

                this.AudioStreams.Clear();
                this.SubtitleStreams.Clear();

                foreach (XmlNode result in track)
                {
                    switch (result.Attributes["type"].Value)
                    {
                        case "General":
                            this.PopulateGeneral(result);
                            break;

                        case "Video":

                            this.PopulateVideoStreams(result);
                            break;

                        case "Text":

                            this.PopulateSubtitleStreams(result);
                            break;

                        case "Audio":

                            this.PopulateAudioStreams(result);
                            break;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, "MiResponse - > PopulateFromXML - > " + ex.Message, ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The populate audio streams.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        private void PopulateAudioStreams(XmlNode result)
        {
            var audioStream = new MiAudioStreamModel();

            foreach (XmlNode xmlNode in result.ChildNodes)
            {
                switch (xmlNode.Name)
                {
                    case "ID":
                        audioStream.ID = xmlNode.InnerText.ToInt();
                        break;

                    case "Format":
                        audioStream.Format = xmlNode.InnerText;
                        break;

                    case "Format_Info":
                        audioStream.Format_Info = xmlNode.InnerText;
                        break;

                    case "Mode_extension":
                        audioStream.FormatSEttingsModeExtension = xmlNode.InnerText;
                        break;

                    case "Codec_ID":
                        audioStream.CodecID = xmlNode.InnerText;
                        break;

                    case "Duration":
                        audioStream.Duration = xmlNode.InnerText;
                        break;

                    case "Bit_rate_mode":
                        audioStream.BitRateMode = xmlNode.InnerText;
                        break;

                    case "Bit_rate":
                        audioStream.Bitrate = xmlNode.InnerText;
                        break;

                    case "Channel_s_":
                        audioStream.Channels = xmlNode.InnerText;
                        break;

                    case "Channel_positions":
                        audioStream.ChannelPositions = xmlNode.InnerText;
                        break;

                    case "Sampling_rate":
                        audioStream.SamplingRate = xmlNode.InnerText;
                        break;

                    case "Bit_depth":
                        audioStream.BitDepth = xmlNode.InnerText;
                        break;

                    case "Compression_mode":
                        audioStream.CompressionMode = xmlNode.InnerText;
                        break;

                    case "Stream_size":
                        audioStream.StreamSize = xmlNode.InnerText;
                        break;
                }
            }

            this.AudioStreams.Add(audioStream);
        }

        /// <summary>
        /// Populates the general details
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        private void PopulateGeneral(XmlNode result)
        {
            foreach (XmlNode xmlNode in result.ChildNodes)
            {
                switch (xmlNode.Name)
                {
                    case "Unique_ID":
                        this.UniqueID = xmlNode.InnerText;
                        break;

                    case "Complete_name":
                        this.CompleteName = xmlNode.InnerText;
                        break;

                    case "Format":
                        this.Format = xmlNode.InnerText;
                        break;

                    case "File_size":
                        this.FileSize = xmlNode.InnerText;
                        break;

                    case "Duration":
                        this.Duration = xmlNode.InnerText;
                        break;

                    case "Overall_bit_rate":
                        this.OverallBitRate = xmlNode.InnerText;
                        break;

                    case "Encoded_date":
                        this.EncodedDate = xmlNode.InnerText;
                        break;

                    case "Encoded_Application":
                        this.EncodedApplication = xmlNode.InnerText;
                        break;

                    case "Encoded_Library_String":
                        this.EncodedLibrary = xmlNode.InnerText;
                        break;

                    case "Writing_application":
                        this.WritingApplication = xmlNode.InnerText;
                        break;

                    case "Writing_library":
                        this.WritingLibrary = xmlNode.InnerText;
                        break;
                }
            }
        }

        /// <summary>
        /// The populate subtitle streams.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        private void PopulateSubtitleStreams(XmlNode result)
        {
            var subtitleStream = new MiSubtitleStreamModel();

            foreach (XmlNode xmlNode in result.ChildNodes)
            {
                switch (xmlNode.Name)
                {
                    case "ID_String":
                        subtitleStream.ID = xmlNode.InnerText.ToInt();
                        break;
                    case "Format":
                        subtitleStream.Format = xmlNode.InnerText;
                        break;
                    case "CodecID":
                        subtitleStream.CodecID = xmlNode.InnerText;
                        break;
                    case "CodecID_Info":
                        subtitleStream.CodecIDInfo = xmlNode.InnerText;
                        break;
                    case "Language_String":
                        subtitleStream.Language = xmlNode.InnerText;
                        break;
                }
            }

            this.SubtitleStreams.Add(subtitleStream);
        }

        /// <summary>
        /// The populate video streams.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        private void PopulateVideoStreams(XmlNode result)
        {
            var videoStream = new MiVideoStreamModel();

            foreach (XmlNode xmlNode in result.ChildNodes)
            {
                switch (xmlNode.Name)
                {
                    case "ID":
                        videoStream.ID = xmlNode.InnerText.ToInt();
                        break;

                    case "Format":
                        videoStream.Format = xmlNode.InnerText;
                        break;

                    case "Format_Info":
                        videoStream.FormatInfo = xmlNode.InnerText;
                        break;

                    case "Format_profile":
                        videoStream.FormatProfile = xmlNode.InnerText;
                        break;

                    case "Format_settings__CABAC":
                        videoStream.FormatSettingsCABAC = xmlNode.InnerText;
                        break;

                    case "Format_settings__ReFrames":
                        videoStream.FormatSettingsRefFrames = xmlNode.InnerText;
                        break;

                    case "Format_Settings_GOP":
                        videoStream.FormatSettingsGOP = xmlNode.InnerText;
                        break;

                    case "Codec_ID":
                        videoStream.CodecID = xmlNode.InnerText;
                        break;

                    case "Duration":
                        videoStream.Duration = xmlNode.InnerText;
                        break;

                    case "Bit_rate":
                        videoStream.BitRate = xmlNode.InnerText;
                        break;

                    case "Width":
                        videoStream.Width = xmlNode.InnerText.Replace(" ", string.Empty).GetNumber(4);
                        break;

                    case "Height":
                        videoStream.Height = xmlNode.InnerText.Replace(" ", string.Empty).GetNumber(4);
                        break;

                    case "Display_aspect_ratio":
                        videoStream.DisplayAspectRatio = xmlNode.InnerText;
                        break;

                    case "Frame_rate":
                        videoStream.FrameRate = xmlNode.InnerText;
                        break;

                    case "Color_space":
                        videoStream.ColorSpace = xmlNode.InnerText;
                        break;

                    case "Chroma_subsampling":
                        videoStream.ChromaSubsampling = xmlNode.InnerText;
                        break;

                    case "Bit_depth":
                        videoStream.BitDepth = xmlNode.InnerText;
                        break;

                    case "Scan_type":
                        videoStream.ScanType = xmlNode.InnerText;
                        break;

                    case "Bits__Pixel_Frame_":
                        videoStream.BitsPixelFrame = xmlNode.InnerText;
                        break;

                    case "Stream_size":
                        videoStream.StreamSize = xmlNode.InnerText;
                        break;

                    case "Writing_library":
                        videoStream.EncodedLibrary = xmlNode.InnerText;
                        break;

                    case "Encoding_settings":
                        videoStream.EncodedLibrarySettings = xmlNode.InnerText;
                        break;

                    case "Language":
                        videoStream.Language = xmlNode.InnerText;
                        break;
                }
            }

            this.VideoStreams.Add(videoStream);
        }

        #endregion
    }
}