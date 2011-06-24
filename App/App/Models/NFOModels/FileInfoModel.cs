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

            this.AudioStreams.ListChanged += (sender, e) => this.OnPropertyChanged("AudioStreams");
            this.SubtitleStreams.ListChanged += (sender, e) => this.OnPropertyChanged("SubtitleStreams");

            this.Codec = string.Empty;
            this.AspectRatioPercent = string.Empty;
            this.AspectRatioDecimal = string.Empty;
            this.FPS = string.Empty;
            this.FPSRounded = string.Empty;
        }

        #endregion

        #region Properties

        private string _codec;

        public string Codec
        {
            get
            {
                return this._codec;
            }
            set
            {
                if (this._codec != value)
                {
                    this._codec = value;
                    this.OnPropertyChanged("Codec");
                }
            }
        }

        private int _width;

        public int Width
        {
            get
            {
                return this._width;
            }

            set
            {
                if (this._width != value)
                {
                    this._width = value;
                    this.OnPropertyChanged("Width");
                }
            }
        }

        private int _height;

        public int Height
        {
            get
            {
                return this._height;
            }

            set
            {
                if (this._height != value)
                {
                    this._height = value;
                    this.OnPropertyChanged("Height");
                }
            }
        }

        private string aspectRatioPercent;

        public string AspectRatioPercent
        {
            get
            {
                return this.aspectRatioPercent;
            }
            set
            {
                if (this.aspectRatioPercent != value)
                {
                    this.aspectRatioPercent = value;
                    this.OnPropertyChanged("AspectRatio");
                }
            }
        }

        private string _aspectRatioDecimal;

        public string AspectRatioDecimal
        {
            get
            {
                return this._aspectRatioDecimal;
            }
            set
            {
                if (this._aspectRatioDecimal != value)
                {
                    this._aspectRatioDecimal = value;
                    this.OnPropertyChanged("AspectRatioDecimal");
                }
            }
        }

        private bool _progressiveScan;

        public bool ProgressiveScan
        {
            get
            {
                return this._progressiveScan;
            }
            set
            {
                if (this._progressiveScan != value)
                {
                    this._progressiveScan = value;
                    this.OnPropertyChanged("ProgressiveScan");
                }
            }
        }

        private bool _interlacedScan;

        public bool InterlacedScan
        {
            get
            {
                return this._interlacedScan;
            }
            set
            {
                if (this._interlacedScan != value)
                {
                    this._interlacedScan = value;
                    this.OnPropertyChanged("InterlacedScan");
                }
                
            }
        }

        private string _fps;

        public string FPS
        {
            get
            {
                return this._fps;
            }
            set
            {
                if (this._fps != value)
                {
                    this._fps = value;
                    this.OnPropertyChanged("FPS");
                }
            }
        }

        private string _fpsRounded;

        public string FPSRounded
        {
            get
            {
                return this._fpsRounded;
            }
            set
            {
                if (this._fpsRounded != value)
                {
                    this._fpsRounded = value;
                    this.OnPropertyChanged("FPSRounded");
                }
            }
        }

        private bool _pal;

        public bool Pal
        {
            get
            {
                return this._pal;
            }
            set
            {
                if (this._pal != value)
                {
                    this._pal = value;
                    this.OnPropertyChanged("Pal");
                }
            }
        }

        private bool _ntsc;

        public bool Ntsc
        {
            get
            {
                return this._ntsc;
            }
            set
            {
                if (this._ntsc != value)
                {
                    this._ntsc = value;
                    this.OnPropertyChanged("Ntsc");
                }
            }
        }

        public string Resolution
        {
            get
            {
                return GetResolution(this.Width);
            }
        }

        private string GetResolution(int width)
        {
            if (width >= Settings.Get.MediaInfo.Resolution480From && width <= Settings.Get.MediaInfo.Resolution480To)
            {
                return "480";
            }

            if (width >= Settings.Get.MediaInfo.Resolution576From && width <= Settings.Get.MediaInfo.Resolution576To)
            {
                return "576";
            }

            if (width >= Settings.Get.MediaInfo.Resolution720From && width <= Settings.Get.MediaInfo.Resolution720To)
            {
                return "720";
            }

            if (width >= Settings.Get.MediaInfo.Resolution1080From && width <= Settings.Get.MediaInfo.Resolution1080To)
            {
                return "1080";
            }

            return string.Empty;
        }

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

        public string ScanType
        {
            get
            {
                if (this.InterlacedScan)
                {
                    return "i";
                }
                else if (this.ProgressiveScan)
                {
                    return "p";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string VideoType
        {
            get
            {
                if (this.Pal)
                {
                    return "PAL";
                }
                else if (this.Ntsc)
                {
                    return "NTSC";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string AspectRatio
        {
            get
            {
                if (Settings.Get.MediaInfo.UsePercentAspectRatio)
                {
                    return AspectRatioPercent;
                }
                else if (Settings.Get.MediaInfo.UseDecimalAspectRatio)
                {
                    return AspectRatioDecimal;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        #endregion
    }
}