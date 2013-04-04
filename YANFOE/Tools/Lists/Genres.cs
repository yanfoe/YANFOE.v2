// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Genres.cs">
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
//   The genres.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Lists
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using YANFOE.Tools.Enums;

    #endregion

    /// <summary>
    ///   The genres.
    /// </summary>
    public static class Genres
    {
        #region Static Fields

        /// <summary>
        ///   The genres collection.
        /// </summary>
        private static readonly Dictionary<ScraperList, List<string>> GenresCollection;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes static members of the <see cref="Genres" /> class.
        /// </summary>
        static Genres()
        {
            GenresCollection = new Dictionary<ScraperList, List<string>>();

            IEnumerable<ScraperList> scraperLists = Enum.GetValues(typeof(ScraperList)).Cast<ScraperList>();

            foreach (ScraperList scraper in scraperLists)
            {
                GenresCollection.Add(scraper, ReadGenreFromXml(scraper));
            }

            GenreFilmUp = new List<string>
                {
                    "Azione", 
                    "Avventura", 
                    "Animazione", 
                    "Biografia", 
                    "Comedy", 
                    "Crime", 
                    "Documentario", 
                    "Drammatico", 
                    "Family", 
                    "Fantasy", 
                    "Film-Noir", 
                    "Game-Show", 
                    "Storia", 
                    "Horror", 
                    "Music", 
                    "Musical", 
                    "Mistero", 
                    "News", 
                    "Reality-TV", 
                    "Romance", 
                    "la fantascienza", 
                    "Short ", 
                    "Sport", 
                    "Talk-Show", 
                    "Thriller", 
                    "Guerra", 
                    "Western"
                };

            GenresCollection.Add(ScraperList.FilmUp, GenreFilmUp);

            GenreFilmWeb = new List<string>
                {
                    "Akcja", 
                    "Animacja", 
                    "Anime", 
                    "Baśń", 
                    "Biblijny", 
                    "Biograficzny", 
                    "Czarna komedia", 
                    "Dla dzieci", 
                    "Dla młodzieży", 
                    "Dokumentalizowany", 
                    "Dokumentalny", 
                    "Dramat", 
                    "Dramat historyczny", 
                    "Dramat obyczajowy", 
                    "Dramat sądowy", 
                    "Dramat społeczny", 
                    "Dreszczowiec", 
                    "Edukacyjny", 
                    "Erotyczny", 
                    "Etiuda", 
                    "Fabularyzowany dok.", 
                    "Familijny", 
                    "Fantasy", 
                    "Film-Noir", 
                    "Gangsterski", 
                    "Groteska filmowa", 
                    "Historyczny", 
                    "Horror", 
                    "Karate", 
                    "Katastroficzny", 
                    "Komedia", 
                    "Komedia dokumentalna", 
                    "Komedia kryminalna", 
                    "Komedia obycz.", 
                    "Komedia rom.", 
                    "Kostiumowy", 
                    "Kr&oacute;tkometrażowy", 
                    "Kryminał", 
                    "Melodramat", 
                    "Musical", 
                    "Muzyczny", 
                    "Niemy", 
                    "Nowele filmowe", 
                    "Obyczajowy", 
                    "Poetycki", 
                    "Polityczny", 
                    "Prawniczy", 
                    "Przygodowy", 
                    "Przyrodniczy", 
                    "Psychologiczny", 
                    "Płaszcza i szpady", 
                    "Religijny", 
                    "Romans", 
                    "Satyra", 
                    "Sci-Fi", 
                    "Sensacyjny", 
                    "Sportowy", 
                    "Surreallistyczny", 
                    "Szpiegowski", 
                    "Sztuki walki", 
                    "Thriller", 
                    "Western", 
                    "Wojenny", 
                    "XXX"
                };

            GenreOfdb = new List<string>
                {
                    "Abenteuer", 
                    "Action", 
                    "Amateur", 
                    "Animation", 
                    "Biographie", 
                    "Dokumentation", 
                    "Drama", 
                    "Eastern", 
                    "Erotik", 
                    "Experimentalfilm", 
                    "Fantasy", 
                    "Grusel", 
                    "Hardcore", 
                    "Heimatfilm", 
                    "Historienfilm", 
                    "Horror", 
                    "Kampfsport", 
                    "Katastrophen", 
                    "Kinder-/Familienfilm", 
                    "Komödie", 
                    "Krieg", 
                    "Krimi", 
                    "Kurzfilm", 
                    "Liebe/Romantik", 
                    "Manga/Anime", 
                    "Mondo", 
                    "Musikfilm", 
                    "Mystery", 
                    "Science-Fiction", 
                    "Sex", 
                    "Splatter", 
                    "Sportfilm", 
                    "TV-Mini-Serie", 
                    "TV-Serie", 
                    "Thriller", 
                    "Tierfilm", 
                    "Trash", 
                    "Western"
                };

            GenreImdb = new List<string>
                {
                    "Action", 
                    "Adventure", 
                    "Animation", 
                    "Biography", 
                    "Comedy", 
                    "Crime", 
                    "Documentary", 
                    "Drama", 
                    "Family", 
                    "Fantasy", 
                    "Film-Noir", 
                    "Game-Show", 
                    "History", 
                    "Horror", 
                    "Music", 
                    "Musical", 
                    "Mystery", 
                    "News", 
                    "Reality-TV", 
                    "Romance", 
                    "Sci-Fi", 
                    "Short", 
                    "Sport", 
                    "Talk-Show", 
                    "Thriller", 
                    "War", 
                    "Western"
                };

            GenreKinopoisk = new List<string>
                {
                    "аниме", 
                    "биография", 
                    "боевик", 
                    "вестерн", 
                    "военный", 
                    "детектив", 
                    "детский", 
                    "для взрослых", 
                    "документальный", 
                    "драма", 
                    "игра", 
                    "история", 
                    "комедия", 
                    "концерт", 
                    "короткометражка", 
                    "криминал", 
                    "мелодрама", 
                    "музыка", 
                    "мультфильм", 
                    "мюзикл", 
                    "новости", 
                    "приключения", 
                    "реальное ТВ", 
                    "семейный", 
                    "спорт", 
                    "ток-шоу", 
                    "триллер", 
                    "ужасы", 
                    "фантастика", 
                    "фильм-нуар", 
                    "фэнтези"
                };

            GenreMovieMeter = new List<string>
                {
                    "Actie", 
                    "Animatie", 
                    "Avontuur", 
                    "Documentaire", 
                    "Drama", 
                    "Familie", 
                    "Fantasy", 
                    "Film-Noir", 
                    "Horror", 
                    "Komedie", 
                    "Mini-series", 
                    "Misdaad", 
                    "Muziek", 
                    "Mystery", 
                    "Oorlog", 
                    "Roadmovie", 
                    "Romantiek", 
                    "Science-Fiction", 
                    "Thriller", 
                    "Western"
                };

            GenreSratim = new List<string>
                {
                    "הלועפ", 
                    "םירגובמ", 
                    "תואקתפרה", 
                    "היצמינא", 
                    "היפרגויב", 
                    "הידמוק", 
                    "עשפ", 
                    "ידועית", 
                    "המרד", 
                    "החפשמ", 
                    "היזטנפ", 
                    "לפא", 
                    "ןועושעש", 
                    "הירוטסיה", 
                    "המיא", 
                    "הקיזומ", 
                    "רמזחמ", 
                    "ןירותסימ", 
                    "תושדח", 
                    "יטילאיר", 
                    "הקיטנמור", 
                    "ינוידב עדמ", 
                    "רצק", 
                    "טרופס", 
                    "חוריא"
                };
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets the genre list for Allocine.
        /// </summary>
        /// <value> The Allocine genre collection. </value>
        private static List<string> GenreAllocine { get; set; }

        /// <summary>
        ///   Gets or sets the genre list for FilmAffinity.
        /// </summary>
        /// <value> The FilmAffinity genre collection. </value>
        private static List<string> GenreFilmAffinity { get; set; }

        /// <summary>
        ///   Gets or sets the genre list for FilmDelta.
        /// </summary>
        /// <value> The FilmDelta genre collection. </value>
        private static List<string> GenreFilmDelta { get; set; }

        /// <summary>
        ///   Gets or sets the genre list for FilmUp.
        /// </summary>
        /// <value> The FilmUp genre collection. </value>
        private static List<string> GenreFilmUp { get; set; }

        /// <summary>
        ///   Gets or sets the genre list for FilmWeb.
        /// </summary>
        /// <value> The FilmWeb genre collection. </value>
        private static List<string> GenreFilmWeb { get; set; }

        /// <summary>
        ///   Gets or sets the genre list for IMDB.
        /// </summary>
        /// <value> The IMDB genre collection. </value>
        private static List<string> GenreImdb { get; set; }

        /// <summary>
        ///   Gets or sets the genre list for Kinopoisk.
        /// </summary>
        /// <value> The Kinopoisk genre collection. </value>
        private static List<string> GenreKinopoisk { get; set; }

        /// <summary>
        ///   Gets or sets the genre list for MovieMeter.
        /// </summary>
        /// <value> The MovieMeter genre collection. </value>
        private static List<string> GenreMovieMeter { get; set; }

        /// <summary>
        ///   Gets or sets the genre list for OFDB.
        /// </summary>
        /// <value> The OFDB genre collection. </value>
        private static List<string> GenreOfdb { get; set; }

        /// <summary>
        ///   Gets or sets the genre list for Sratim.
        /// </summary>
        /// <value> The Sratim genre collection. </value>
        private static List<string> GenreSratim { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Get genre list.
        /// </summary>
        /// <param name="scraperList">
        /// The scraper list. 
        /// </param>
        /// <returns>
        /// Scraper list genres 
        /// </returns>
        public static List<string> GetGenreList(ScraperList scraperList = ScraperList.Imdb)
        {
            if (GenresCollection[scraperList] == null)
            {
                return new List<string>();
            }

            return GenresCollection[scraperList];
        }

        /// <summary>
        /// Reads the genre from XML.
        /// </summary>
        /// <param name="scraperListType">
        /// Type of the scraper list. 
        /// </param>
        /// <returns>
        /// Genre collection 
        /// </returns>
        public static List<string> ReadGenreFromXml(ScraperList scraperListType)
        {
            try
            {
                var doc = new XDocument(XDocument.Load("Xml/Genre/" + scraperListType + ".xml"));

                IEnumerable<string> q = from x in doc.Descendants("genre") select x.Value;

                return q.ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        #endregion
    }
}