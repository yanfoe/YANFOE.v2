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

            this.VideoStreams = new BindingList<MiVideoStreamModel>();
            this.AudioStreams = new BindingList<MiAudioStreamModel>();
            this.SubtitleStreams = new BindingList<MiSubtitleStreamModel>();
        }

        #endregion

        #region Properties

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
                    case "ID_String":
                        audioStream.ID = xmlNode.InnerText.ToInt();
                        break;

                    case "Format":
                        audioStream.Format = xmlNode.InnerText;
                        break;

                    case "Format_Info":
                        audioStream.Format_Info = xmlNode.InnerText;
                        break;

                    case "Format_Settings_ModeExtension":
                        audioStream.FormatSEttingsModeExtension = xmlNode.InnerText;
                        break;

                    case "CodecID":
                        audioStream.CodecID = xmlNode.InnerText;
                        break;

                    case "Duration_String":
                        audioStream.Duration = xmlNode.InnerText;
                        break;

                    case "BitRate_Mode_String":
                        audioStream.BitRateMode = xmlNode.InnerText;
                        break;

                    case "BitRate_String":
                        audioStream.Bitrate = xmlNode.InnerText;
                        break;

                    case "Channel_s__String":
                        audioStream.Channels = xmlNode.InnerText;
                        break;

                    case "ChannelPositions":
                        audioStream.ChannelPositions = xmlNode.InnerText;
                        break;

                    case "SamplingRate_String":
                        audioStream.SamplingRate = xmlNode.InnerText;
                        break;

                    case "BitDepth_String":
                        audioStream.BitDepth = xmlNode.InnerText;
                        break;

                    case "Compression_Mode_String":
                        audioStream.CompressionMode = xmlNode.InnerText;
                        break;

                    case "StreamSize_String":
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
                    case "UniqueID_String":
                        this.UniqueID = xmlNode.InnerText;
                        break;

                    case "CompleteName":
                        this.CompleteName = xmlNode.InnerText;
                        break;

                    case "Format":
                        this.Format = xmlNode.InnerText;
                        break;

                    case "FileSize_String":
                        this.FileSize = xmlNode.InnerText;
                        break;

                    case "Duration_String":
                        this.Duration = xmlNode.InnerText;
                        break;

                    case "OverallBitRate_String":
                        this.OverallBitRate = xmlNode.InnerText;
                        break;

                    case "Encoded_Date":
                        this.EncodedDate = xmlNode.InnerText;
                        break;

                    case "Encoded_Application":
                        this.EncodedApplication = xmlNode.InnerText;
                        break;

                    case "Encoded_Library_String":
                        this.EncodedLibrary = xmlNode.InnerText;
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
                    case "ID_String":
                        videoStream.ID = xmlNode.InnerText.ToInt();
                        break;

                    case "Format":
                        videoStream.Format = xmlNode.InnerText;
                        break;

                    case "Format_Info":
                        videoStream.FormatInfo = xmlNode.InnerText;
                        break;

                    case "Format_Profile":
                        videoStream.FormatProfile = xmlNode.InnerText;
                        break;

                    case "Format_Settings_CABAC_String":
                        videoStream.FormatSettingsCABAC = xmlNode.InnerText;
                        break;

                    case "Format_Settings_RefFrames_String":
                        videoStream.FormatSettingsRefFrames = xmlNode.InnerText;
                        break;

                    case "Format_Settings_GOP":
                        videoStream.FormatSettingsGOP = xmlNode.InnerText;
                        break;

                    case "CodecID":
                        videoStream.CodecID = xmlNode.InnerText;
                        break;

                    case "Duration_String":
                        videoStream.Duration = xmlNode.InnerText;
                        break;

                    case "BitRate_String":
                        videoStream.BitRate = xmlNode.InnerText;
                        break;

                    case "Width_String":
                        videoStream.Width = xmlNode.InnerText;
                        break;

                    case "Height_String":
                        videoStream.Height = xmlNode.InnerText;
                        break;

                    case "DisplayAspectRatio_String":
                        videoStream.DisplayAspectRatio = xmlNode.InnerText;
                        break;

                    case "FrameRate_String":
                        videoStream.FrameRate = xmlNode.InnerText;
                        break;

                    case "ColorSpace":
                        videoStream.ColorSpace = xmlNode.InnerText;
                        break;

                    case "ChromaSubsampling":
                        videoStream.ChromaSubsampling = xmlNode.InnerText;
                        break;

                    case "BitDepth_String":
                        videoStream.BitDepth = xmlNode.InnerText;
                        break;

                    case "ScanType_String":
                        videoStream.ScanType = xmlNode.InnerText;
                        break;

                    case "Bits-_Pixel_Frame_":
                        videoStream.BitsPixelFrame = xmlNode.InnerText;
                        break;

                    case "StreamSize_String":
                        videoStream.StreamSize = xmlNode.InnerText;
                        break;

                    case "Encoded_Library_String":
                        videoStream.EncodedLibrary = xmlNode.InnerText;
                        break;

                    case "Encoded_Library_Settings":
                        videoStream.EncodedLibrarySettings = xmlNode.InnerText;
                        break;

                    case "Language_String":
                        videoStream.Language = xmlNode.InnerText;
                        break;
                }
            }

            this.VideoStreams.Add(videoStream);
        }

        #endregion
    }
}