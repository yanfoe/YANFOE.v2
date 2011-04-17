// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieMeterApiHandler.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.ThirdParty
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

    using CookComputing.XmlRpc;

    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.IO;
    using YANFOE.Scrapers.Movie;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Xml;

    public class MovieMeterApiHandler
    {
        private static IMMApi apiProxy;
        private static string url = "http://www.moviemeter.nl/ws";

        private static string sessionKey = "";
        private static int sessionValidTill = 0;

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

        /// <summary>
        /// Requests for a new sessionkey of it's not initialized yet or not valid anymore
        /// otherwise the known sessionkey will be used
        /// </summary>
        /// <returns>String with a valid sessionkey</returns>
        private static string getSessionKey()
        {
            if ((new DateTime(1970, 1, 1)).AddSeconds(sessionValidTill) < DateTime.Now.ToUniversalTime())
            {
                apiProxy = (IMMApi)XmlRpcProxyGen.Create(typeof(IMMApi));
                ApiStartSession s = apiProxy.StartSession(Settings.ConstSettings.Application.MovieMeterApi);
                sessionKey = s.session_key;
                sessionValidTill = s.valid_till;
            }

            return sessionKey;
        }


        /// <summary>
        /// Definitions for the method-results
        /// In these classes more functionality can be added (like conversion to int or date)
        /// </summary>
        public class ApiStartSession
        {
            public string session_key;
            public int valid_till;
            public string disclaimer;
        }

        public class ApiError
        {
            public string faultCode;
            public string faultString;
        }

        public class Film
        {
            public string filmId;
            public string url;
            public string title;
            public string alternative_title;
            public string year;
            public string average;
            public string votes_count;
            public string similarity;
        }

        public class FilmScore
        {
            public string votes;
            public string total;
            public string average;
        }

        public class FilmImdb
        {
            public string code;
            public string url;
            public string score;
            public int votes;
        }

        public class FilmDetail
        {
            public string url;
            public string thumbnail;
            public string title;
            public Title[] alternative_titles;
            public string year;
            public string imdb;
            public string plot;
            public string duration;
            public Duration[] durations;
            public Actor[] actors;
            public string actors_text;
            public Director[] directors;
            public string directors_text;
            public Country[] countries;
            public string countries_text;
            public string[] genres;
            public string genres_text;
            public Date[] dates_cinema;
            public Date[] dates_video;
            public string average;
            public string votes_count;
            public int filmId;

            public class Duration
            {
                public string duration;
                public string description;
            }

            public class Actor
            {
                public string name;
                public string voice;
            }
            public class Director
            {
                public string id;
                public string name;
            }
            public class Country
            {
                public string iso_3166_1;
                public string name;
            }
            public class Date
            {
                public string date;
            }
            public class Title
            {
                public string title;
            }
        }

        public class Director
        {
            public string directorId;
            public string url;
            public string name;
            public string similarity;
        }

        public class DirectorDetail
        {
            public string url;
            public string thumbnail;
            public string name;
            public string born;
            public string deceased;
        }

        public class DirectorFilm
        {
            public string filmId;
            public string url;
            public string title;
            public string alternative_title;
            public string year;
        }

        /// <summary>
        /// The XmlRpcMethod representation of the available methods
        /// </summary>
        [XmlRpcUrl("http://www.moviemeter.nl/ws")]
        public interface IMMApi : IXmlRpcProxy
        {
            [XmlRpcMethod("api.startSession")]
            ApiStartSession StartSession(string apikey);

            [XmlRpcMethod("api.closeSession")]
            bool CloseSession(string sessionkey);

            [XmlRpcMethod("film.search")]
            Film[] Search(string sessionkey, string search);

            [XmlRpcMethod("film.retrieveScore")]
            FilmScore RetrieveScore(string sessionkey, int filmId);

            [XmlRpcMethod("film.retrieveImdb")]
            FilmImdb RetrieveImdb(string sessionkey, int filmId);

            [XmlRpcMethod("film.retrieveByImdb")]
            string RetrieveByImdb(string sessionkey, string imdb_code);

            [XmlRpcMethod("film.retrieveDetails")]
            FilmDetail RetrieveDetails(string sessionkey, int filmId);

            [XmlRpcMethod("director.search")]
            Director[] DirectorSearch(string sessionkey, string search);

            [XmlRpcMethod("director.retrieveDetails")]
            DirectorDetail DirectorRetrieveDetails(string sessionkey, int directorId);

            [XmlRpcMethod("director.retrieveFilms")]
            DirectorFilm[] DirectorRetrieveFilms(string sessionkey, int directorId);
        }
    }
}
