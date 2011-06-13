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

    using YANFOE.Factories.Apps.MediaInfo.Models;
    using YANFOE.Models.GeneralModels;
    using YANFOE.Tools.Models;

    /// <summary>
    /// File Codec Info Model
    /// </summary>
    [Serializable]
    public class FileInfoModel : ModelBase
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfoModel"/> class.
        /// </summary>
        public FileInfoModel()
        {


            this.AudioStreams = new BindingList<MiAudioStreamModel>();
            this.SubtitleStreams = new BindingList<MiSubtitleStreamModel>();
        }

        #endregion

        #region Properties

        public string Codec { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string AspectRatio { get; set; }

        public string AspectRatioDecimal { get; set; }

        public bool ProgressiveScan { get; set; }

        public bool InterlacedScan { get; set; }

        public string FPS { get; set; }

        public string FPSRounded { get; set; }

        public bool Pal { get; set; }

        public bool Ntsc { get; set; }

        public string Resolution { get; set; }

        /// <summary>
        /// Gets or sets the audio stream.
        /// </summary>
        /// <value>The audio stream.</value>
        public BindingList<MiAudioStreamModel> AudioStreams { get; set; }

        /// <summary>
        /// Gets or sets the subtitle stream.
        /// </summary>
        /// <value>The subtitle stream.</value>
        public BindingList<MiSubtitleStreamModel> SubtitleStreams { get; set; }

        #endregion
    }
}