// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiAudioStreamModel.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Factories.Apps.MediaInfo.Models
{
    using System;

    /// <summary>
    /// The MediaInfo audio stream model
    /// </summary>
    [Serializable]
    public class MiAudioStreamModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MiAudioStreamModel"/> class.
        /// </summary>
        public MiAudioStreamModel()
        {
            this.Format = string.Empty;
            this.FormatInfo = string.Empty;
            this.FormatVersion = string.Empty;
            this.FormatProfile = string.Empty;
            this.FormatSettingsMode = string.Empty;
            this.FormatSEttingsModeExtension = string.Empty;
            this.CodecID = string.Empty;
            this.CodecIDHint = string.Empty;
            this.Duration = string.Empty;
            this.BitRateMode = string.Empty;
            this.Bitrate = string.Empty;
            this.BitDepth = string.Empty;
            this.NominalBitRate = string.Empty;
            this.Channels = string.Empty;
            this.ChannelPositions = string.Empty;
            this.SamplingRate = string.Empty;
            this.VideoDelay = string.Empty;
            this.StreamSize = string.Empty;
            this.Alignment = string.Empty;
            this.InterleaveDuration = string.Empty;
            this.InterleavePreloadDuration = string.Empty;
            this.WritingLibrary = string.Empty;
            this.Language = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Alignment.
        /// </summary>
        public string Alignment { get; set; }

        /// <summary>
        /// Gets or sets BitRateMode.
        /// </summary>
        public string BitRateMode { get; set; }

        /// <summary>
        /// Gets or sets Bitrate.
        /// </summary>
        public string Bitrate { get; set; }

        /// <summary>
        /// Gets or sets the bit depth.
        /// </summary>
        public string BitDepth { get; set; }

        /// <summary>
        /// Gets or sets ChannelPositions.
        /// </summary>
        public string ChannelPositions { get; set; }

        /// <summary>
        /// Gets or sets Channels.
        /// </summary>
        public string Channels { get; set; }

        /// <summary>
        /// Gets or sets CodecID.
        /// </summary>
        public string CodecID { get; set; }

        /// <summary>
        /// Gets or sets CodecIDHint.
        /// </summary>
        public string CodecIDHint { get; set; }

        /// <summary>
        /// Gets or sets Duration.
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets Format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets FormatProfile.
        /// </summary>
        public string FormatProfile { get; set; }

        /// <summary>
        /// Gets or sets FormatSEttingsModeExtension.
        /// </summary>
        public string FormatSEttingsModeExtension { get; set; }

        /// <summary>
        /// Gets or sets FormatSettingsMode.
        /// </summary>
        public string FormatSettingsMode { get; set; }

        /// <summary>
        /// Gets or sets FormatVersion.
        /// </summary>
        public string FormatVersion { get; set; }

        /// <summary>
        /// Gets or sets Format_Info.
        /// </summary>
        public string FormatInfo { get; set; }

        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets InterleaveDuration.
        /// </summary>
        public string InterleaveDuration { get; set; }

        /// <summary>
        /// Gets or sets InterleavePreloadDuration.
        /// </summary>
        public string InterleavePreloadDuration { get; set; }

        /// <summary>
        /// Gets or sets Language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets NominalBitRate.
        /// </summary>
        public string NominalBitRate { get; set; }

        /// <summary>
        /// Gets or sets SamplingRate.
        /// </summary>
        public string SamplingRate { get; set; }

        /// <summary>
        /// Gets or sets StreamSize.
        /// </summary>
        public string StreamSize { get; set; }

        /// <summary>
        /// Gets or sets VideoDelay.
        /// </summary>
        public string VideoDelay { get; set; }

        /// <summary>
        /// Gets or sets WritingLibrary.
        /// </summary>
        public string WritingLibrary { get; set; }

        public string CompressionMode { get; set; }

        #endregion
    }
}