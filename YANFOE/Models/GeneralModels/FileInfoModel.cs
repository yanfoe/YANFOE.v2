// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="FileInfoModel.cs">
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
//   The file info model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.GeneralModels
{
    #region Required Namespaces

    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    /// The file info model.
    /// </summary>
    public class FileInfoModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfoModel"/> class.
        /// </summary>
        public FileInfoModel()
        {
            this.AudioStreams = new ThreadedBindingList<AudioStreamModel>();
            this.SubtitleStreams = new ThreadedBindingList<SubtitleStreamModel>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the aspect ratio decimal.
        /// </summary>
        public string AspectRatioDecimal { get; set; }

        /// <summary>
        /// Gets or sets the aspect ratio percentage.
        /// </summary>
        public string AspectRatioPercentage { get; set; }

        /// <summary>
        /// Gets or sets the audio streams.
        /// </summary>
        public ThreadedBindingList<AudioStreamModel> AudioStreams { get; set; }

        /// <summary>
        /// Gets or sets the codec.
        /// </summary>
        public string Codec { get; set; }

        /// <summary>
        /// Gets or sets the fps.
        /// </summary>
        public string FPS { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether interlaced.
        /// </summary>
        public bool Interlaced { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether NTSC.
        /// </summary>
        public bool NTSC { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pal.
        /// </summary>
        public bool PAL { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether progressive.
        /// </summary>
        public bool Progressive { get; set; }

        /// <summary>
        /// Gets or sets the resolution.
        /// </summary>
        public string Resolution { get; set; }

        /// <summary>
        /// Gets or sets the subtitle streams.
        /// </summary>
        public ThreadedBindingList<SubtitleStreamModel> SubtitleStreams { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public string Width { get; set; }

        #endregion
    }
}