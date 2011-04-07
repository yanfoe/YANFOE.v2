// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileInfoModel.cs" company="The YANFOE Project">
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
//   File Codec Info Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Models.NFOModels
{
    using System;
    using System.ComponentModel;

    using YANFOE.Tools.Models;

    /// <summary>
    /// File Codec Info Model
    /// </summary>
    [Serializable]
    public class FileInfoModel : ModelBase
    {
        #region Constants and Fields

        /// <summary>
        /// VideoOutput backing field
        /// </summary>
        private string videoOutput;

        /// <summary>
        /// Videosource backing field
        /// </summary>
        private string videoSource;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfoModel"/> class.
        /// </summary>
        public FileInfoModel()
        {
            this.VideoSource = string.Empty;
            this.VideoOutput = string.Empty;
            this.VideoStream = new BindingList<VideoStream>();
            this.AudioStream = new BindingList<AudioStreamModel>();
            this.SubtitleStream = new BindingList<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the audio stream.
        /// </summary>
        /// <value>The audio stream.</value>
        public BindingList<AudioStreamModel> AudioStream { get; set; }

        /// <summary>
        /// Gets or sets the subtitle stream.
        /// </summary>
        /// <value>The subtitle stream.</value>
        public BindingList<string> SubtitleStream { get; set; }

        /// <summary>
        /// Gets or sets the video output.
        /// </summary>
        /// <value>The video output.</value>
        public string VideoOutput
        {
            get
            {
                return this.videoOutput;
            }

            set
            {
                this.videoOutput = value;
                this.OnPropertyChanged("VideoOutput");
            }
        }

        /// <summary>
        /// Gets or sets the video source.
        /// </summary>
        /// <value>The video source.</value>
        public string VideoSource
        {
            get
            {
                return this.videoSource;
            }

            set
            {
                this.videoSource = value;
                this.OnPropertyChanged("VideoSource");
            }
        }

        /// <summary>
        /// Gets or sets the video stream.
        /// </summary>
        /// <value>The video stream.</value>
        public BindingList<VideoStream> VideoStream { get; set; }

        #endregion
    }
}