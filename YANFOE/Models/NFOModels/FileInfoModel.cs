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
//   File Codec Info Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.NFOModels
{
    #region Required Namespaces

    using System;

    using YANFOE.Factories.Apps.MediaInfo.Models;
    using YANFOE.Settings;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   File Codec Info Model
    /// </summary>
    [Serializable]
    public class FileInfoModel : ModelBase
    {
        #region Fields

        /// <summary>
        /// The _aspect ratio decimal.
        /// </summary>
        private string _aspectRatioDecimal;

        /// <summary>
        /// The _codec.
        /// </summary>
        private string _codec;

        /// <summary>
        /// The _fps.
        /// </summary>
        private string _fps;

        /// <summary>
        /// The _fps rounded.
        /// </summary>
        private string _fpsRounded;

        /// <summary>
        /// The _height.
        /// </summary>
        private int _height;

        /// <summary>
        /// The _interlaced scan.
        /// </summary>
        private bool _interlacedScan;

        /// <summary>
        /// The _ntsc.
        /// </summary>
        private bool _ntsc;

        /// <summary>
        /// The _pal.
        /// </summary>
        private bool _pal;

        /// <summary>
        /// The _progressive scan.
        /// </summary>
        private bool _progressiveScan;

        /// <summary>
        /// The _width.
        /// </summary>
        private int _width;

        /// <summary>
        /// The aspect ratio percent.
        /// </summary>
        private string aspectRatioPercent;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="FileInfoModel" /> class.
        /// </summary>
        public FileInfoModel()
        {
            this.AudioStreams = new ThreadedBindingList<MiAudioStreamModel>();
            this.SubtitleStreams = new ThreadedBindingList<MiSubtitleStreamModel>();

            this.AudioStreams.ListChanged += (sender, e) => this.OnPropertyChanged("AudioStreams");
            this.SubtitleStreams.ListChanged += (sender, e) => this.OnPropertyChanged("SubtitleStreams");

            this.Codec = string.Empty;
            this.AspectRatioPercent = string.Empty;
            this.AspectRatioDecimal = string.Empty;
            this.FPS = string.Empty;
            this.FPSRounded = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the aspect ratio.
        /// </summary>
        public string AspectRatio
        {
            get
            {
                if (Get.MediaInfo.UsePercentAspectRatio)
                {
                    return this.AspectRatioPercent;
                }
                else if (Get.MediaInfo.UseDecimalAspectRatio)
                {
                    return this.AspectRatioDecimal;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the aspect ratio decimal.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the aspect ratio percent.
        /// </summary>
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

        /// <summary>
        ///   Gets or sets the audio stream.
        /// </summary>
        /// <value> The audio stream. </value>
        public ThreadedBindingList<MiAudioStreamModel> AudioStreams { get; set; }

        /// <summary>
        /// Gets or sets the codec.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the fps.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the fps rounded.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether interlaced scan.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether ntsc.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether pal.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether progressive scan.
        /// </summary>
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

        /// <summary>
        /// Gets the resolution.
        /// </summary>
        public string Resolution
        {
            get
            {
                return this.GetResolution(this.Width);
            }
        }

        /// <summary>
        /// Gets the scan type.
        /// </summary>
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

        /// <summary>
        ///   Gets or sets the subtitle stream.
        /// </summary>
        /// <value> The subtitle stream. </value>
        public ThreadedBindingList<MiSubtitleStreamModel> SubtitleStreams { get; set; }

        /// <summary>
        /// Gets the video type.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
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

        #endregion

        #region Methods

        /// <summary>
        /// The get resolution.
        /// </summary>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetResolution(int width)
        {
            if (width >= Get.MediaInfo.Resolution480From && width <= Get.MediaInfo.Resolution480To)
            {
                return "480";
            }

            if (width >= Get.MediaInfo.Resolution576From && width <= Get.MediaInfo.Resolution576To)
            {
                return "576";
            }

            if (width >= Get.MediaInfo.Resolution720From && width <= Get.MediaInfo.Resolution720To)
            {
                return "720";
            }

            if (width >= Get.MediaInfo.Resolution1080From && width <= Get.MediaInfo.Resolution1080To)
            {
                return "1080";
            }

            return string.Empty;
        }

        #endregion
    }
}