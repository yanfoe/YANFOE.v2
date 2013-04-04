// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MovieMeterApiHandler.cs">
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
//   The movie meter api handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.ThirdParty
{
    #region Required Namespaces

    using System;
    using System.Xml;

    using CookComputing.XmlRpc;

    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.IO;
    using YANFOE.Scrapers.Movie;
    using YANFOE.Settings.ConstSettings;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Xml;

    #endregion

    /// <summary>
    /// The movie meter api handler.
    /// </summary>
    public class MovieMeterApiHandler
    {
        #region Static Fields

        /// <summary>
        /// The api proxy.
        /// </summary>
        private static IMMApi apiProxy;

        /// <summary>
        /// The session key.
        /// </summary>
        private static string sessionKey = string.Empty;

        /// <summary>
        /// The session valid till.
        /// </summary>
        private static int sessionValidTill;

        #endregion

        #region Interfaces

        /// <summary>
        ///   The XmlRpcMethod representation of the available methods
        /// </summary>
        [XmlRpcUrl("http://www.moviemeter.nl/ws")]
        public interface IMMApi : IXmlRpcProxy
        {
            #region Public Methods and Operators

            /// <summary>
            /// The close session.
            /// </summary>
            /// <param name="sessionkey">
            /// The sessionkey.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            [XmlRpcMethod("api.closeSession")]
            bool CloseSession(string sessionkey);

            /// <summary>
            /// The director retrieve details.
            /// </summary>
            /// <param name="sessionkey">
            /// The sessionkey.
            /// </param>
            /// <param name="directorId">
            /// The director id.
            /// </param>
            /// <returns>
            /// The <see cref="DirectorDetail"/>.
            /// </returns>
            [XmlRpcMethod("director.retrieveDetails")]
            DirectorDetail DirectorRetrieveDetails(string sessionkey, int directorId);

            /// <summary>
            /// The director retrieve films.
            /// </summary>
            /// <param name="sessionkey">
            /// The sessionkey.
            /// </param>
            /// <param name="directorId">
            /// The director id.
            /// </param>
            /// <returns>
            /// The <see cref="DirectorFilm[]"/>.
            /// </returns>
            [XmlRpcMethod("director.retrieveFilms")]
            DirectorFilm[] DirectorRetrieveFilms(string sessionkey, int directorId);

            /// <summary>
            /// The director search.
            /// </summary>
            /// <param name="sessionkey">
            /// The sessionkey.
            /// </param>
            /// <param name="search">
            /// The search.
            /// </param>
            /// <returns>
            /// The <see cref="Director[]"/>.
            /// </returns>
            [XmlRpcMethod("director.search")]
            Director[] DirectorSearch(string sessionkey, string search);

            /// <summary>
            /// The retrieve by imdb.
            /// </summary>
            /// <param name="sessionkey">
            /// The sessionkey.
            /// </param>
            /// <param name="imdb_code">
            /// The imdb_code.
            /// </param>
            /// <returns>
            /// The <see cref="string"/>.
            /// </returns>
            [XmlRpcMethod("film.retrieveByImdb")]
            string RetrieveByImdb(string sessionkey, string imdb_code);

            /// <summary>
            /// The retrieve details.
            /// </summary>
            /// <param name="sessionkey">
            /// The sessionkey.
            /// </param>
            /// <param name="filmId">
            /// The film id.
            /// </param>
            /// <returns>
            /// The <see cref="FilmDetail"/>.
            /// </returns>
            [XmlRpcMethod("film.retrieveDetails")]
            FilmDetail RetrieveDetails(string sessionkey, int filmId);

            /// <summary>
            /// The retrieve imdb.
            /// </summary>
            /// <param name="sessionkey">
            /// The sessionkey.
            /// </param>
            /// <param name="filmId">
            /// The film id.
            /// </param>
            /// <returns>
            /// The <see cref="FilmImdb"/>.
            /// </returns>
            [XmlRpcMethod("film.retrieveImdb")]
            FilmImdb RetrieveImdb(string sessionkey, int filmId);

            /// <summary>
            /// The retrieve score.
            /// </summary>
            /// <param name="sessionkey">
            /// The sessionkey.
            /// </param>
            /// <param name="filmId">
            /// The film id.
            /// </param>
            /// <returns>
            /// The <see cref="FilmScore"/>.
            /// </returns>
            [XmlRpcMethod("film.retrieveScore")]
            FilmScore RetrieveScore(string sessionkey, int filmId);

            /// <summary>
            /// The search.
            /// </summary>
            /// <param name="sessionkey">
            /// The sessionkey.
            /// </param>
            /// <param name="search">
            /// The search.
            /// </param>
            /// <returns>
            /// The <see cref="Film[]"/>.
            /// </returns>
            [XmlRpcMethod("film.search")]
            Film[] Search(string sessionkey, string search);

            /// <summary>
            /// The start session.
            /// </summary>
            /// <param name="apikey">
            /// The apikey.
            /// </param>
            /// <returns>
            /// The <see cref="ApiStartSession"/>.
            /// </returns>
            [XmlRpcMethod("api.startSession")]
            ApiStartSession StartSession(string apikey);

            #endregion
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The generate movie meter xml.
        /// </summary>
        /// <param name="downloadItem">
        /// The download item.
        /// </param>
        /// <param name="idType">
        /// The id type.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GenerateMovieMeterXml(DownloadItem downloadItem, string idType, string value)
        {
            var apiProxy = (IMMApi)XmlRpcProxyGen.Create(typeof(IMMApi));

            const string imdb = "imdb";

            const string search = "search";

            var filmDetail = new FilmDetail();

            if (idType == imdb)
            {
                value = apiProxy.RetrieveByImdb(getSessionKey(), value);
            }
            else if (idType == search)
            {
                var filmlist = apiProxy.Search(getSessionKey(), value);
                if (filmlist.Length > 0)
                {
                    value = filmlist[0].filmId;
                }
                else
                {
                    return string.Empty;
                }
            }

            filmDetail = apiProxy.RetrieveDetails(getSessionKey(), value.ToInt());

            var movieMeter = new MovieMeter();

            using (var stringWriter = new StringWriterWithEncoding(movieMeter.HtmlEncoding))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                {
                    xmlWriter.WriteStartElement("moviemeter");

                    XWrite.WriteEnclosedElement(xmlWriter, "id", filmDetail.filmId);
                    XWrite.WriteEnclosedElement(xmlWriter, "imdbid", filmDetail.imdb);

                    XWrite.WriteEnclosedElement(xmlWriter, "title", filmDetail.title);
                    XWrite.WriteEnclosedElement(xmlWriter, "year", filmDetail.year);

                    foreach (var actor in filmDetail.actors)
                    {
                        XWrite.WriteEnclosedElement(xmlWriter, "actor", actor.name);
                    }

                    foreach (var genre in filmDetail.genres)
                    {
                        XWrite.WriteEnclosedElement(xmlWriter, "genre", genre);
                    }

                    XWrite.WriteEnclosedElement(xmlWriter, "plot", filmDetail.plot);

                    foreach (var actor in filmDetail.directors)
                    {
                        XWrite.WriteEnclosedElement(xmlWriter, "director", actor.name);
                    }

                    XWrite.WriteEnclosedElement(xmlWriter, "releasedate", filmDetail.dates_cinema[0].date);

                    foreach (var country in filmDetail.countries)
                    {
                        XWrite.WriteEnclosedElement(xmlWriter, "country", country.name, "code", country.iso_3166_1);
                    }

                    XWrite.WriteEnclosedElement(xmlWriter, "countries", filmDetail.countries_text);
                    XWrite.WriteEnclosedElement(xmlWriter, "rating", filmDetail.average);

                    xmlWriter.WriteEndElement();
                }

                return stringWriter.ToString();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Requests for a new sessionkey of it's not initialized yet or not valid anymore
        ///   otherwise the known sessionkey will be used
        /// </summary>
        /// <returns> String with a valid sessionkey </returns>
        private static string getSessionKey()
        {
            if ((new DateTime(1970, 1, 1)).AddSeconds(sessionValidTill) < DateTime.Now.ToUniversalTime())
            {
                apiProxy = (IMMApi)XmlRpcProxyGen.Create(typeof(IMMApi));
                ApiStartSession s = apiProxy.StartSession(Application.MovieMeterApi);
                sessionKey = s.session_key;
                sessionValidTill = s.valid_till;
            }

            return sessionKey;
        }

        #endregion

        /// <summary>
        /// The api error.
        /// </summary>
        public class ApiError
        {
            #region Fields

            /// <summary>
            /// The fault code.
            /// </summary>
            public string faultCode;

            /// <summary>
            /// The fault string.
            /// </summary>
            public string faultString;

            #endregion
        }

        /// <summary>
        ///   Definitions for the method-results
        ///   In these classes more functionality can be added (like conversion to int or date)
        /// </summary>
        public class ApiStartSession
        {
            #region Fields

            /// <summary>
            /// The disclaimer.
            /// </summary>
            public string disclaimer;

            /// <summary>
            /// The session_key.
            /// </summary>
            public string session_key;

            /// <summary>
            /// The valid_till.
            /// </summary>
            public int valid_till;

            #endregion
        }

        /// <summary>
        /// The director.
        /// </summary>
        public class Director
        {
            #region Fields

            /// <summary>
            /// The director id.
            /// </summary>
            public string directorId;

            /// <summary>
            /// The name.
            /// </summary>
            public string name;

            /// <summary>
            /// The similarity.
            /// </summary>
            public string similarity;

            /// <summary>
            /// The url.
            /// </summary>
            public string url;

            #endregion
        }

        /// <summary>
        /// The director detail.
        /// </summary>
        public class DirectorDetail
        {
            #region Fields

            /// <summary>
            /// The born.
            /// </summary>
            public string born;

            /// <summary>
            /// The deceased.
            /// </summary>
            public string deceased;

            /// <summary>
            /// The name.
            /// </summary>
            public string name;

            /// <summary>
            /// The thumbnail.
            /// </summary>
            public string thumbnail;

            /// <summary>
            /// The url.
            /// </summary>
            public string url;

            #endregion
        }

        /// <summary>
        /// The director film.
        /// </summary>
        public class DirectorFilm
        {
            #region Fields

            /// <summary>
            /// The alternative_title.
            /// </summary>
            public string alternative_title;

            /// <summary>
            /// The film id.
            /// </summary>
            public string filmId;

            /// <summary>
            /// The title.
            /// </summary>
            public string title;

            /// <summary>
            /// The url.
            /// </summary>
            public string url;

            /// <summary>
            /// The year.
            /// </summary>
            public string year;

            #endregion
        }

        /// <summary>
        /// The film.
        /// </summary>
        public class Film
        {
            #region Fields

            /// <summary>
            /// The alternative_title.
            /// </summary>
            public string alternative_title;

            /// <summary>
            /// The average.
            /// </summary>
            public string average;

            /// <summary>
            /// The film id.
            /// </summary>
            public string filmId;

            /// <summary>
            /// The similarity.
            /// </summary>
            public string similarity;

            /// <summary>
            /// The title.
            /// </summary>
            public string title;

            /// <summary>
            /// The url.
            /// </summary>
            public string url;

            /// <summary>
            /// The votes_count.
            /// </summary>
            public string votes_count;

            /// <summary>
            /// The year.
            /// </summary>
            public string year;

            #endregion
        }

        /// <summary>
        /// The film detail.
        /// </summary>
        public class FilmDetail
        {
            #region Fields

            /// <summary>
            /// The actors.
            /// </summary>
            public Actor[] actors;

            /// <summary>
            /// The actors_text.
            /// </summary>
            public string actors_text;

            /// <summary>
            /// The alternative_titles.
            /// </summary>
            public Title[] alternative_titles;

            /// <summary>
            /// The average.
            /// </summary>
            public string average;

            /// <summary>
            /// The countries.
            /// </summary>
            public Country[] countries;

            /// <summary>
            /// The countries_text.
            /// </summary>
            public string countries_text;

            /// <summary>
            /// The dates_cinema.
            /// </summary>
            public Date[] dates_cinema;

            /// <summary>
            /// The dates_video.
            /// </summary>
            public Date[] dates_video;

            /// <summary>
            /// The directors.
            /// </summary>
            public Director[] directors;

            /// <summary>
            /// The directors_text.
            /// </summary>
            public string directors_text;

            /// <summary>
            /// The duration.
            /// </summary>
            public string duration;

            /// <summary>
            /// The durations.
            /// </summary>
            public Duration[] durations;

            /// <summary>
            /// The film id.
            /// </summary>
            public int filmId;

            /// <summary>
            /// The genres.
            /// </summary>
            public string[] genres;

            /// <summary>
            /// The genres_text.
            /// </summary>
            public string genres_text;

            /// <summary>
            /// The imdb.
            /// </summary>
            public string imdb;

            /// <summary>
            /// The plot.
            /// </summary>
            public string plot;

            /// <summary>
            /// The thumbnail.
            /// </summary>
            public string thumbnail;

            /// <summary>
            /// The title.
            /// </summary>
            public string title;

            /// <summary>
            /// The url.
            /// </summary>
            public string url;

            /// <summary>
            /// The votes_count.
            /// </summary>
            public string votes_count;

            /// <summary>
            /// The year.
            /// </summary>
            public string year;

            #endregion

            /// <summary>
            /// The actor.
            /// </summary>
            public class Actor
            {
                #region Fields

                /// <summary>
                /// The name.
                /// </summary>
                public string name;

                /// <summary>
                /// The voice.
                /// </summary>
                public string voice;

                #endregion
            }

            /// <summary>
            /// The country.
            /// </summary>
            public class Country
            {
                #region Fields

                /// <summary>
                /// The iso_3166_1.
                /// </summary>
                public string iso_3166_1;

                /// <summary>
                /// The name.
                /// </summary>
                public string name;

                #endregion
            }

            /// <summary>
            /// The date.
            /// </summary>
            public class Date
            {
                #region Fields

                /// <summary>
                /// The date.
                /// </summary>
                public string date;

                #endregion
            }

            /// <summary>
            /// The director.
            /// </summary>
            public class Director
            {
                #region Fields

                /// <summary>
                /// The id.
                /// </summary>
                public string id;

                /// <summary>
                /// The name.
                /// </summary>
                public string name;

                #endregion
            }

            /// <summary>
            /// The duration.
            /// </summary>
            public class Duration
            {
                #region Fields

                /// <summary>
                /// The description.
                /// </summary>
                public string description;

                /// <summary>
                /// The duration.
                /// </summary>
                public string duration;

                #endregion
            }

            /// <summary>
            /// The title.
            /// </summary>
            public class Title
            {
                #region Fields

                /// <summary>
                /// The title.
                /// </summary>
                public string title;

                #endregion
            }
        }

        /// <summary>
        /// The film imdb.
        /// </summary>
        public class FilmImdb
        {
            #region Fields

            /// <summary>
            /// The code.
            /// </summary>
            public string code;

            /// <summary>
            /// The score.
            /// </summary>
            public string score;

            /// <summary>
            /// The url.
            /// </summary>
            public string url;

            /// <summary>
            /// The votes.
            /// </summary>
            public int votes;

            #endregion
        }

        /// <summary>
        /// The film score.
        /// </summary>
        public class FilmScore
        {
            #region Fields

            /// <summary>
            /// The average.
            /// </summary>
            public string average;

            /// <summary>
            /// The total.
            /// </summary>
            public string total;

            /// <summary>
            /// The votes.
            /// </summary>
            public string votes;

            #endregion
        }
    }
}