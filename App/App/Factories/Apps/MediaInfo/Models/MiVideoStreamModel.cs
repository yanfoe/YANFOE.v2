// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiVideoStreamModel.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   The mi video stream model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Factories.Apps.MediaInfo.Models
{
    using System;

    /// <summary>
    /// The mi video stream model.
    /// </summary>
    [Serializable]
    public class MiVideoStreamModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MiVideoStreamModel"/> class.
        /// </summary>
        public MiVideoStreamModel()
        {
            this.Format = string.Empty;
            this.FormatInfo = string.Empty;
            this.FormatProfile = string.Empty;
            this.FormatSettingsCABAC = string.Empty;
            this.FormatSettingsGOP = string.Empty;
            this.FormatSettingsRefFrames = string.Empty;
            this.CodecID = string.Empty;
            this.Duration = string.Empty;
            this.BitRate = string.Empty;
            this.DisplayAspectRatio = string.Empty;
            this.FrameRate = string.Empty;
            this.ColorSpace = string.Empty;
            this.ChromaSubsampling = string.Empty;
            this.BitDepth = string.Empty;
            this.ScanType = string.Empty;
            this.BitsPixelFrame = string.Empty;
            this.StreamSize = string.Empty;
            this.EncodedLibrary = string.Empty;
            this.EncodedLibrarySettings = string.Empty;
            this.Language = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets BitDepth.
        /// </summary>
        public string BitDepth { get; set; }

        /// <summary>
        /// Gets or sets BitRate.
        /// </summary>
        public string BitRate { get; set; }

        /// <summary>
        /// Gets or sets BitsPixelFrame.
        /// </summary>
        public string BitsPixelFrame { get; set; }

        /// <summary>
        /// Gets or sets ChromaSubsampling.
        /// </summary>
        public string ChromaSubsampling { get; set; }

        /// <summary>
        /// Gets or sets CodecID.
        /// </summary>
        public string CodecID { get; set; }

        /// <summary>
        /// Gets or sets ColorSpace.
        /// </summary>
        public string ColorSpace { get; set; }

        /// <summary>
        /// Gets or sets DisplayAspectRatio.
        /// </summary>
        public string DisplayAspectRatio { get; set; }

        /// <summary>
        /// Gets or sets Duration.
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets EncodedLibrary.
        /// </summary>
        public string EncodedLibrary { get; set; }

        /// <summary>
        /// Gets or sets EncodedLibrarySettings.
        /// </summary>
        public string EncodedLibrarySettings { get; set; }

        /// <summary>
        /// Gets or sets Format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets FormatInfo.
        /// </summary>
        public string FormatInfo { get; set; }

        /// <summary>
        /// Gets or sets FormatProfile.
        /// </summary>
        public string FormatProfile { get; set; }

        /// <summary>
        /// Gets or sets FormatSettingsCABAC.
        /// </summary>
        public string FormatSettingsCABAC { get; set; }

        /// <summary>
        /// Gets or sets FormatSettingsGOP.
        /// </summary>
        public string FormatSettingsGOP { get; set; }

        /// <summary>
        /// Gets or sets FormatSettingsRefFrames.
        /// </summary>
        public string FormatSettingsRefFrames { get; set; }

        /// <summary>
        /// Gets or sets FrameRate.
        /// </summary>
        public string FrameRate { get; set; }

        /// <summary>
        /// Gets or sets Height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets Language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets ScanType.
        /// </summary>
        public string ScanType { get; set; }

        /// <summary>
        /// Gets or sets StreamSize.
        /// </summary>
        public string StreamSize { get; set; }

        /// <summary>
        /// Gets or sets Width.
        /// </summary>
        public int Width { get; set; }

        #endregion
    }
}