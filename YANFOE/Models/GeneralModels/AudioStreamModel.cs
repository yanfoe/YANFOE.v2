// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="AudioStreamModel.cs">
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
//   A Audio stream model for use attached to a MediaScanOutput
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.GeneralModels
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   A Audio stream model for use attached to a MediaScanOutput
    /// </summary>
    [Serializable]
    public class AudioStreamModel
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="AudioStreamModel" /> class.
        /// </summary>
        public AudioStreamModel()
        {
            this.Format = string.Empty;
            this.Format_Info = string.Empty;
            this.FormatVersion = string.Empty;
            this.FormatProfile = string.Empty;
            this.FormatSettingsMode = string.Empty;
            this.FormatSEttingsModeExtension = string.Empty;
            this.CodecID = string.Empty;
            this.CodecIDHint = string.Empty;
            this.Duration = string.Empty;
            this.BitRateMode = string.Empty;
            this.Bitrate = string.Empty;
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
        public string CodecID { get; set; }

        /// <summary>
        ///   Gets or sets CodecIDHint.
        /// </summary>
        public string CodecIDHint { get; set; }

        /// <summary>
        ///   Gets or sets Duration.
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        ///   Gets or sets Format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        ///   Gets or sets FormatProfile.
        /// </summary>
        public string FormatProfile { get; set; }

        /// <summary>
        ///   Gets or sets FormatSettingsModeExtension.
        /// </summary>
        public string FormatSEttingsModeExtension { get; set; }

        /// <summary>
        ///   Gets or sets FormatSettingsMode.
        /// </summary>
        public string FormatSettingsMode { get; set; }

        /// <summary>
        ///   Gets or sets FormatVersion.
        /// </summary>
        public string FormatVersion { get; set; }

        /// <summary>
        ///   Gets or sets Format_Info.
        /// </summary>
        public string Format_Info { get; set; }

        /// <summary>
        ///   Gets or sets ID.
        /// </summary>
        public int ID { get; set; }

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