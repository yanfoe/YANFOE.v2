// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MovieSaveSettings.cs">
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
//   Settings related to saving movie nfo and images.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.IOModels
{
    #region Required Namespaces

    using System;

    using YANFOE.Factories.InOut.Enum;
    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    ///   Settings related to saving movie nfo and images.
    /// </summary>
    [Serializable]
    public class MovieSaveSettings : ModelBase
    {
        #region Fields

        /// <summary>
        ///   The bluray fanart name template.
        /// </summary>
        private string blurayFanartNameTemplate;

        /// <summary>
        ///   The bluray nfo name template.
        /// </summary>
        private string blurayNfoNameTemplate;

        /// <summary>
        ///   The bluray poster name template.
        /// </summary>
        private string blurayPosterNameTemplate;

        /// <summary>
        ///   The bluray set fanart name template.
        /// </summary>
        private string bluraySetFanartNameTemplate;

        /// <summary>
        ///   The bluray set poster name template.
        /// </summary>
        private string bluraySetPosterNameTemplate;

        /// <summary>
        ///   The bluray trailer name template.
        /// </summary>
        private string blurayTrailerNameTemplate;

        /// <summary>
        ///   The dvd fanart name template.
        /// </summary>
        private string dvdFanartNameTemplate;

        /// <summary>
        ///   The dvd nfo name template.
        /// </summary>
        private string dvdNfoNameTemplate;

        /// <summary>
        ///   The dvd poster name template.
        /// </summary>
        private string dvdPosterNameTemplate;

        /// <summary>
        ///   The dvd set fanart name template.
        /// </summary>
        private string dvdSetFanartNameTemplate;

        /// <summary>
        ///   The dvd set poster name template.
        /// </summary>
        private string dvdSetPosterNameTemplate;

        /// <summary>
        ///   The dvd trailer name template.
        /// </summary>
        private string dvdTrailerNameTemplate;

        /// <summary>
        ///   The image path.
        /// </summary>
        private string imagePath;

        /// <summary>
        ///   The nfo path.
        /// </summary>
        private string nfoPath;

        /// <summary>
        ///   The normal fanart name template.
        /// </summary>
        private string normalFanartNameTemplate;

        /// <summary>
        ///   The normal nfo name template.
        /// </summary>
        private string normalNfoNameTemplate;

        /// <summary>
        ///   The normal poster name template.
        /// </summary>
        private string normalPosterNameTemplate;

        /// <summary>
        ///   The normal set fanart name template.
        /// </summary>
        private string normalSetFanartNameTemplate;

        /// <summary>
        ///   The normal set poster name template.
        /// </summary>
        private string normalSetPosterNameTemplate;

        /// <summary>
        ///   The normal trailer name template.
        /// </summary>
        private string normalTrailerNameTemplate;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="MovieSaveSettings" /> class.
        /// </summary>
        public MovieSaveSettings()
        {
            this.NfoPath = string.Empty;
            this.ImagePath = string.Empty;

            this.NormalNfoNameTemplate = string.Empty;
            this.NormalPosterNameTemplate = string.Empty;
            this.NormalFanartNameTemplate = string.Empty;
            this.NormalTrailerNameTemplate = string.Empty;
            this.NormalSetPosterNameTemplate = string.Empty;
            this.NormalSetFanartNameTemplate = string.Empty;

            this.DvdNfoNameTemplate = string.Empty;
            this.DvdPosterNameTemplate = string.Empty;
            this.DvdFanartNameTemplate = string.Empty;
            this.DvdTrailerNameTemplate = string.Empty;
            this.DvdSetPosterNameTemplate = string.Empty;
            this.DvdSetFanartNameTemplate = string.Empty;

            this.BlurayNfoNameTemplate = string.Empty;
            this.BlurayPosterNameTemplate = string.Empty;
            this.BlurayFanartNameTemplate = string.Empty;
            this.BlurayTrailerNameTemplate = string.Empty;
            this.BluraySetPosterNameTemplate = string.Empty;
            this.BluraySetFanartNameTemplate = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the bluray fanart name template.
        /// </summary>
        /// <value> The bluray fanart name template. </value>
        public string BlurayFanartNameTemplate
        {
            get
            {
                return this.blurayFanartNameTemplate;
            }

            set
            {
                this.blurayFanartNameTemplate = value;
                this.OnPropertyChanged("BlurayFanartNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the bluray nfo name template.
        /// </summary>
        /// <value> The bluray nfo name template. </value>
        public string BlurayNfoNameTemplate
        {
            get
            {
                return this.blurayNfoNameTemplate;
            }

            set
            {
                this.blurayNfoNameTemplate = value;
                this.OnPropertyChanged("BlurayNfoNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the bluray poster name template.
        /// </summary>
        /// <value> The bluray poster name template. </value>
        public string BlurayPosterNameTemplate
        {
            get
            {
                return this.blurayPosterNameTemplate;
            }

            set
            {
                this.blurayPosterNameTemplate = value;
                this.OnPropertyChanged("BlurayPosterNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the bluray set fanart name template.
        /// </summary>
        /// <value> The bluray set fanart name template. </value>
        public string BluraySetFanartNameTemplate
        {
            get
            {
                return this.bluraySetFanartNameTemplate;
            }

            set
            {
                this.bluraySetFanartNameTemplate = value;
                this.OnPropertyChanged("BluraySetFanartNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the bluray set poster name template.
        /// </summary>
        /// <value> The bluray set poster name template. </value>
        public string BluraySetPosterNameTemplate
        {
            get
            {
                return this.bluraySetPosterNameTemplate;
            }

            set
            {
                this.bluraySetPosterNameTemplate = value;
                this.OnPropertyChanged("BluraySetPosterNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the bluray trailer name template.
        /// </summary>
        /// <value> The bluray trailer name template. </value>
        public string BlurayTrailerNameTemplate
        {
            get
            {
                return this.blurayTrailerNameTemplate;
            }

            set
            {
                this.blurayTrailerNameTemplate = value;
                this.OnPropertyChanged("BlurayTrailerNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the DVD fanart name template.
        /// </summary>
        /// <value> The DVD fanart name template. </value>
        public string DvdFanartNameTemplate
        {
            get
            {
                return this.dvdFanartNameTemplate;
            }

            set
            {
                this.dvdFanartNameTemplate = value;
                this.OnPropertyChanged("DvdFanartNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the DVD nfo name template.
        /// </summary>
        /// <value> The DVD nfo name template. </value>
        public string DvdNfoNameTemplate
        {
            get
            {
                return this.dvdNfoNameTemplate;
            }

            set
            {
                this.dvdNfoNameTemplate = value;
                this.OnPropertyChanged("DvdNfoNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the DVD poster name template.
        /// </summary>
        /// <value> The DVD poster name template. </value>
        public string DvdPosterNameTemplate
        {
            get
            {
                return this.dvdPosterNameTemplate;
            }

            set
            {
                this.dvdPosterNameTemplate = value;
                this.OnPropertyChanged("DvdPosterNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the DVD set fanart name template.
        /// </summary>
        /// <value> The DVD set fanart name template. </value>
        public string DvdSetFanartNameTemplate
        {
            get
            {
                return this.dvdSetFanartNameTemplate;
            }

            set
            {
                this.dvdSetFanartNameTemplate = value;
                this.OnPropertyChanged("DvdSetFanartNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the DVD set poster name template.
        /// </summary>
        /// <value> The DVD set poster name template. </value>
        public string DvdSetPosterNameTemplate
        {
            get
            {
                return this.dvdSetPosterNameTemplate;
            }

            set
            {
                this.dvdSetPosterNameTemplate = value;
                this.OnPropertyChanged("DvdSetPosterNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the DVD trailer name template.
        /// </summary>
        /// <value> The DVD trailer name template. </value>
        public string DvdTrailerNameTemplate
        {
            get
            {
                return this.dvdTrailerNameTemplate;
            }

            set
            {
                this.dvdTrailerNameTemplate = value;
                this.OnPropertyChanged("DvdTrailerNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the path which to save Images too. If with movie leave blank
        /// </summary>
        /// <value> The image path. </value>
        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }

            set
            {
                this.imagePath = value;
                this.OnPropertyChanged("ImagePath");
            }
        }

        /// <summary>
        ///   Gets or sets IoType.
        /// </summary>
        public MovieIOType IoType { get; set; }

        /// <summary>
        ///   Gets or sets the path which to save NFO too. If with movie leave blank
        /// </summary>
        /// <value> The nfo path. </value>
        public string NfoPath
        {
            get
            {
                return this.nfoPath;
            }

            set
            {
                this.nfoPath = value;
                this.OnPropertyChanged("NFOPath");
            }
        }

        /// <summary>
        ///   Gets or sets the fanart name template.
        /// </summary>
        /// <value> The fanart name template. </value>
        public string NormalFanartNameTemplate
        {
            get
            {
                return this.normalFanartNameTemplate;
            }

            set
            {
                this.normalFanartNameTemplate = value;
                this.OnPropertyChanged("NormalFanartNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the nfo name template.
        /// </summary>
        /// <value> The nfo name template. </value>
        public string NormalNfoNameTemplate
        {
            get
            {
                return this.normalNfoNameTemplate;
            }

            set
            {
                this.normalNfoNameTemplate = value;
                this.OnPropertyChanged("NormalNfoNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the poster name template.
        /// </summary>
        /// <value> The poster name template. </value>
        public string NormalPosterNameTemplate
        {
            get
            {
                return this.normalPosterNameTemplate;
            }

            set
            {
                this.normalPosterNameTemplate = value;
                this.OnPropertyChanged("NormalPosterNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the normal set fanart name template.
        /// </summary>
        /// <value> The normal set fanart name template. </value>
        public string NormalSetFanartNameTemplate
        {
            get
            {
                return this.normalSetFanartNameTemplate;
            }

            set
            {
                this.normalSetFanartNameTemplate = value;
                this.OnPropertyChanged("NormalSetFanartNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the normal set poster name template.
        /// </summary>
        /// <value> The normal set poster name template. </value>
        public string NormalSetPosterNameTemplate
        {
            get
            {
                return this.normalSetPosterNameTemplate;
            }

            set
            {
                this.normalSetPosterNameTemplate = value;
                this.OnPropertyChanged("NormalSetPosterNameTemplate");
            }
        }

        /// <summary>
        ///   Gets or sets the trailer name template.
        /// </summary>
        /// <value> The trailer name template. </value>
        public string NormalTrailerNameTemplate
        {
            get
            {
                return this.normalTrailerNameTemplate;
            }

            set
            {
                this.normalTrailerNameTemplate = value;
                this.OnPropertyChanged("NormalTrailerNameTemplate");
            }
        }

        #endregion
    }
}