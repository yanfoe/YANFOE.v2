// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MiAudioStreamModel.cs">
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
//   The MediaInfo audio stream model
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Apps.MediaInfo.Models
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   The MediaInfo audio stream model
    /// </summary>
    [Serializable]
    public class MiAudioStreamModel
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="MiAudioStreamModel" /> class.
        /// </summary>
        public MiAudioStreamModel()
        {
            this.Format = string.Empty;
            this.FormatInfo = string.Empty;
            this.FormatVersion = string.Empty;
            this.FormatProfile = string.Empty;
            this.FormatSettingsMode = string.Empty;
            this.FormatSettingsModeExtension = string.Empty;
            this.CodecId = string.Empty;
            this.CodecIdHint = string.Empty;
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

        #region Public Properties

        /// <summary>
        ///   Gets or sets Alignment.
        /// </summary>
        public string Alignment { get; set; }

        /// <summary>
        ///   Gets or sets the bit depth.
        /// </summary>
        public string BitDepth { get; set; }

        /// <summary>
        ///   Gets or sets BitRateMode.
        /// </summary>
        public string BitRateMode { get; set; }

        /// <summary>
        ///   Gets or sets Bitrate.
        /// </summary>
        public string Bitrate { get; set; }

        /// <summary>
        ///   Gets or sets ChannelPositions.
        /// </summary>
        public string ChannelPositions { get; set; }

        /// <summary>
        ///   Gets or sets Channels.
        /// </summary>
        public string Channels { get; set; }

        /// <summary>
        ///   Gets or sets CodecID.
        /// </summary>
        public string CodecId { get; set; }

        /// <summary>
        ///   Gets or sets CodecIDHint.
        /// </summary>
        public string CodecIdHint { get; set; }

        /// <summary>
        /// Gets or sets the compression mode.
        /// </summary>
        public string CompressionMode { get; set; }

        /// <summary>
        ///   Gets or sets Duration.
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        ///   Gets or sets Format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        ///   Gets or sets Format_Info.
        /// </summary>
        public string FormatInfo { get; set; }

        /// <summary>
        ///   Gets or sets FormatProfile.
        /// </summary>
        public string FormatProfile { get; set; }

        /// <summary>
        ///   Gets or sets FormatSettingsModeExtension.
        /// </summary>
        public string FormatSettingsModeExtension { get; set; }

        /// <summary>
        ///   Gets or sets FormatSettingsMode.
        /// </summary>
        public string FormatSettingsMode { get; set; }

        /// <summary>
        ///   Gets or sets FormatVersion.
        /// </summary>
        public string FormatVersion { get; set; }

        /// <summary>
        ///   Gets or sets ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///   Gets or sets InterleaveDuration.
        /// </summary>
        public string InterleaveDuration { get; set; }

        /// <summary>
        ///   Gets or sets InterleavePreloadDuration.
        /// </summary>
        public string InterleavePreloadDuration { get; set; }

        /// <summary>
        ///   Gets or sets Language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        ///   Gets or sets NominalBitRate.
        /// </summary>
        public string NominalBitRate { get; set; }

        /// <summary>
        ///   Gets or sets SamplingRate.
        /// </summary>
        public string SamplingRate { get; set; }

        /// <summary>
        ///   Gets or sets StreamSize.
        /// </summary>
        public string StreamSize { get; set; }

        /// <summary>
        ///   Gets or sets VideoDelay.
        /// </summary>
        public string VideoDelay { get; set; }

        /// <summary>
        ///   Gets or sets WritingLibrary.
        /// </summary>
        public string WritingLibrary { get; set; }

        #endregion
    }
}