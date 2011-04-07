// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieScraperGroupModel.cs" company="The YANFOE Project">
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
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Scrapers.Movie.Models.ScraperGroup
{
    using System;

    using DevExpress.XtraEditors.DXErrorProvider;

    using YANFOE.Tools.Models;

    /// <summary>
    /// The movie scraper group model.
    /// </summary>
    [Serializable]
    public class MovieScraperGroupModel : ModelBase, IDXDataErrorInfo
    {
        #region Constants and Fields

        /// <summary>
        /// The cast field.
        /// </summary>
        private string cast;

        /// <summary>
        /// The certification.
        /// </summary>
        private string certification;

        /// <summary>
        /// The country.
        /// </summary>
        private string country;

        /// <summary>
        /// The director.
        /// </summary>
        private string director;

        /// <summary>
        /// The fanart.
        /// </summary>
        private string fanart;

        /// <summary>
        /// The genre.
        /// </summary>
        private string genre;

        /// <summary>
        /// The language.
        /// </summary>
        private string language;

        /// <summary>
        /// The mpaa field.
        /// </summary>
        private string mpaa;

        /// <summary>
        /// The origional title.
        /// </summary>
        private string origionalTitle;

        /// <summary>
        /// The outline.
        /// </summary>
        private string outline;

        /// <summary>
        /// The plot field.
        /// </summary>
        private string plot;

        /// <summary>
        /// The poster.
        /// </summary>
        private string poster;

        /// <summary>
        /// The rating.
        /// </summary>
        private string rating;

        /// <summary>
        /// The release date.
        /// </summary>
        private string releaseDate;

        /// <summary>
        /// The runtime.
        /// </summary>
        private string runtime;

        /// <summary>
        /// The scraper description.
        /// </summary>
        private string scraperDescription;

        /// <summary>
        /// The scraper name.
        /// </summary>
        private string scraperName;

        /// <summary>
        /// The studio.
        /// </summary>
        private string studio;

        /// <summary>
        /// The tagline.
        /// </summary>
        private string tagline;

        /// <summary>
        /// The title.
        /// </summary>
        private string title;

        /// <summary>
        /// The top 250.
        /// </summary>
        private string top250;

        /// <summary>
        /// The trailer.
        /// </summary>
        private string trailer;

        /// <summary>
        /// The votes.
        /// </summary>
        private string votes;

        /// <summary>
        /// The writers.
        /// </summary>
        private string writers;

        /// <summary>
        /// The year field.
        /// </summary>
        private string year;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieScraperGroupModel"/> class.
        /// </summary>
        public MovieScraperGroupModel()
        {
            this.title = string.Empty;
            this.year = string.Empty;
            this.origionalTitle = string.Empty;
            this.rating = string.Empty;
            this.tagline = string.Empty;
            this.plot = string.Empty;
            this.outline = string.Empty;
            this.certification = string.Empty;
            this.director = string.Empty;
            this.country = string.Empty;
            this.studio = string.Empty;
            this.language = string.Empty;
            this.genre = string.Empty;
            this.runtime = string.Empty;
            this.top250 = string.Empty;
            this.releaseDate = string.Empty;
            this.votes = string.Empty;
            this.cast = string.Empty;
            this.mpaa = string.Empty;
            this.writers = string.Empty;
            this.fanart = string.Empty;
            this.poster = string.Empty;
            this.trailer = string.Empty;

            this.ClearScrapers();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Cast.
        /// </summary>
        public string Cast
        {
            get
            {
                return this.cast;
            }

            set
            {
                if (this.cast != value)
                {
                    this.cast = value;
                    this.OnPropertyChanged("Cast");
                }
            }
        }

        /// <summary>
        /// Gets or sets Certification.
        /// </summary>
        public string Certification
        {
            get
            {
                return this.certification;
            }

            set
            {
                if (this.certification != value)
                {
                    this.certification = value;
                    this.OnPropertyChanged("Certification");
                }
            }
        }

        /// <summary>
        /// Gets or sets Country.
        /// </summary>
        public string Country
        {
            get
            {
                return this.country;
            }

            set
            {
                if (this.country != value)
                {
                    this.country = value;
                    this.OnPropertyChanged("Country");
                }
            }
        }

        /// <summary>
        /// Gets or sets Director.
        /// </summary>
        public string Director
        {
            get
            {
                return this.director;
            }

            set
            {
                if (this.Director != value)
                {
                    this.director = value;
                    this.OnPropertyChanged("Director");
                }
            }
        }

        /// <summary>
        /// Gets or sets Fanart.
        /// </summary>
        public string Fanart
        {
            get
            {
                return this.fanart;
            }

            set
            {
                if (this.fanart != value)
                {
                    this.fanart = value;
                    this.OnPropertyChanged("Fanart");
                }
            }
        }

        /// <summary>
        /// Gets or sets Genre.
        /// </summary>
        public string Genre
        {
            get
            {
                return this.genre;
            }

            set
            {
                if (this.genre != value)
                {
                    this.genre = value;
                    this.OnPropertyChanged("genre");
                }
            }
        }

        /// <summary>
        /// Gets or sets Language.
        /// </summary>
        public string Language
        {
            get
            {
                return this.language;
            }

            set
            {
                if (this.language != value)
                {
                    this.language = value;
                    this.OnPropertyChanged("Language");
                }
            }
        }

        /// <summary>
        /// Gets or sets Mpaa.
        /// </summary>
        public string Mpaa
        {
            get
            {
                return this.mpaa;
            }

            set
            {
                if (this.mpaa != value)
                {
                    this.mpaa = value;
                    this.OnPropertyChanged("Mpaa");
                }
            }
        }

        /// <summary>
        /// Gets or sets OrigionalTitle.
        /// </summary>
        public string OrigionalTitle
        {
            get
            {
                return this.origionalTitle;
            }

            set
            {
                if (this.origionalTitle != value)
                {
                    this.origionalTitle = value;
                    this.OnPropertyChanged("OrigionalTitle");
                }
            }
        }

        /// <summary>
        /// Gets or sets Outline.
        /// </summary>
        public string Outline
        {
            get
            {
                return this.outline;
            }

            set
            {
                if (this.outline != value)
                {
                    this.outline = value;
                    this.OnPropertyChanged("Outline");
                }
            }
        }

        /// <summary>
        /// Gets or sets Plot.
        /// </summary>
        public string Plot
        {
            get
            {
                return this.plot;
            }

            set
            {
                if (this.plot != value)
                {
                    this.plot = value;
                    this.OnPropertyChanged("Plot");
                }
            }
        }

        /// <summary>
        /// Gets or sets Poster.
        /// </summary>
        public string Poster
        {
            get
            {
                return this.poster;
            }

            set
            {
                if (this.poster != value)
                {
                    this.poster = value;
                    this.OnPropertyChanged("Poster");
                }
            }
        }

        /// <summary>
        /// Gets or sets Rating.
        /// </summary>
        public string Rating
        {
            get
            {
                return this.rating;
            }

            set
            {
                if (this.rating != value)
                {
                    this.rating = value;
                    this.OnPropertyChanged("Rating");
                }
            }
        }

        /// <summary>
        /// Gets or sets ReleaseDate.
        /// </summary>
        public string ReleaseDate
        {
            get
            {
                return this.releaseDate;
            }

            set
            {
                if (this.releaseDate != value)
                {
                    this.releaseDate = value;
                    this.OnPropertyChanged("ReleaseDate");
                }
            }
        }

        /// <summary>
        /// Gets or sets Runtime.
        /// </summary>
        public string Runtime
        {
            get
            {
                return this.runtime;
            }

            set
            {
                if (this.runtime != value)
                {
                    this.runtime = value;
                    this.OnPropertyChanged("Runtime");
                }
            }
        }

        /// <summary>
        /// Gets or sets ScraperDescription.
        /// </summary>
        public string ScraperDescription
        {
            get
            {
                return this.scraperDescription;
            }

            set
            {
                if (this.scraperDescription != value)
                {
                    this.scraperDescription = value;
                    this.OnPropertyChanged("ScraperDescription");
                }
            }
        }

        /// <summary>
        /// Gets or sets FilePath.
        /// </summary>
        public string ScraperName
        {
            get
            {
                return this.scraperName;
            }

            set
            {
                if (this.scraperName != value)
                {
                    this.scraperName = value;
                    this.OnPropertyChanged("ScraperName");
                }
            }
        }

        /// <summary>
        /// Gets or sets Studio.
        /// </summary>
        public string Studio
        {
            get
            {
                return this.studio;
            }

            set
            {
                if (this.studio != value)
                {
                    this.studio = value;
                    this.OnPropertyChanged("Studio");
                }
            }
        }

        /// <summary>
        /// Gets or sets Tagline.
        /// </summary>
        public string Tagline
        {
            get
            {
                return this.tagline;
            }

            set
            {
                if (this.tagline != value)
                {
                    this.tagline = value;
                    this.OnPropertyChanged("Tagline");
                }
            }
        }

        /// <summary>
        /// Gets or sets Title.
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        /// <summary>
        /// Gets or sets Top250.
        /// </summary>
        public string Top250
        {
            get
            {
                return this.top250;
            }

            set
            {
                if (this.top250 != value)
                {
                    this.top250 = value;
                    this.OnPropertyChanged("Top250");
                }
            }
        }

        /// <summary>
        /// Gets or sets Trailer.
        /// </summary>
        public string Trailer
        {
            get
            {
                return this.trailer;
            }

            set
            {
                if (this.trailer != value)
                {
                    this.trailer = value;
                    this.OnPropertyChanged("Trailer");
                }
            }
        }

        /// <summary>
        /// Gets or sets Votes.
        /// </summary>
        public string Votes
        {
            get
            {
                return this.votes;
            }

            set
            {
                this.votes = value;
                this.OnPropertyChanged("Votes");
            }
        }

        /// <summary>
        /// Gets or sets Writers.
        /// </summary>
        public string Writers
        {
            get
            {
                return this.writers;
            }

            set
            {
                if (this.writers != value)
                {
                    this.writers = value;
                    this.OnPropertyChanged("Writers");
                }
            }
        }

        /// <summary>
        /// Gets or sets Year.
        /// </summary>
        public string Year
        {
            get
            {
                return this.year;
            }

            set
            {
                if (this.year != value)
                {
                    this.year = value;
                    this.OnPropertyChanged("Year");
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The clear scrapers.
        /// </summary>
        public void ClearScrapers()
        {
            this.ScraperDescription = string.Empty;

            this.Title = string.Empty;
            this.Year = string.Empty;
            this.OrigionalTitle = string.Empty;
            this.Rating = string.Empty;
            this.Tagline = string.Empty;
            this.Plot = string.Empty;
            this.Outline = string.Empty;
            this.Certification = string.Empty;
            this.Director = string.Empty;
            this.Country = string.Empty;
            this.Studio = string.Empty;
            this.Language = string.Empty;
            this.Genre = string.Empty;
            this.Runtime = string.Empty;
            this.Top250 = string.Empty;
            this.Writers = string.Empty;
            this.ReleaseDate = string.Empty;
            this.Votes = string.Empty;
            this.Cast = string.Empty;
            this.Fanart = string.Empty;
            this.Poster = string.Empty;
            this.Trailer = string.Empty;
        }

        #endregion

        #region Implemented Interfaces

        #region IDXDataErrorInfo

        /// <summary>
        /// When implemented by a class, this method returns information on an error associated with a business object.
        /// </summary>
        /// <param name="info">An <see cref="T:DevExpress.XtraEditors.DXErrorProvider.ErrorInfo"/> object that contains information on an error.</param>
        public void GetError(ErrorInfo info)
        {
        }

        /// <summary>
        /// When implemented by a class, this method returns information on an error associated with a specific business object's property.
        /// </summary>
        /// <param name="propertyName">A string that identifies the name of the property for which information on an error is to be returned.</param>
        /// <param name="info">An <see cref="T:DevExpress.XtraEditors.DXErrorProvider.ErrorInfo"/> object that contains information on an error.</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            switch (propertyName)
            {
                case "ScraperName":
                    if (string.IsNullOrEmpty(this.ScraperName))
                    {
                        info.ErrorText = "Please enter scraper group name.";
                        info.ErrorType = ErrorType.Critical;
                    }

                    break;

                case "ScraperDescription":
                    if (string.IsNullOrEmpty(this.ScraperDescription))
                    {
                        info.ErrorText = "Please enter scraper group description.";
                        info.ErrorType = ErrorType.Critical;
                    }

                    break;
            }
        }

        #endregion

        #endregion
    }
}