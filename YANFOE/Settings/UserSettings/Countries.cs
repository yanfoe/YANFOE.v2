// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Countries.cs">
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
//   The countries.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Settings.UserSettings
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;

    using YANFOE.Tools.Enums;

    #endregion

    /// <summary>
    ///   The countries.
    /// </summary>
    [Serializable]
    public class Countries
    {
        #region Fields

        /// <summary>
        ///   The imdb country list
        /// </summary>
        private List<string> imdb;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Countries" /> class.
        /// </summary>
        public Countries()
        {
            this.CountryDictionary = new Dictionary<ScraperList, List<string>>();

            this.AddImdb();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the country dictionary.
        /// </summary>
        /// <value> The country dictionary. </value>
        public Dictionary<ScraperList, List<string>> CountryDictionary { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///   The add imdb.
        /// </summary>
        private void AddImdb()
        {
            this.imdb = new List<string>
                {
                    "Albania", 
                    "Argentina", 
                    "Australia", 
                    "Austria", 
                    "Belgium", 
                    "Brazil", 
                    "Bulgaria", 
                    "Canada", 
                    "Chile", 
                    "China", 
                    "Colombia", 
                    "Croatia", 
                    "Cuba", 
                    "Czech Republic", 
                    "Czechoslovakia", 
                    "Denmark", 
                    "East Germany", 
                    "Egypt", 
                    "Finland", 
                    "France", 
                    "Georgia", 
                    "Germany", 
                    "Greece", 
                    "Hong Kong", 
                    "Hungary", 
                    "India", 
                    "Indonesia", 
                    "Iran", 
                    "Ireland", 
                    "Israel", 
                    "Italy", 
                    "Japan", 
                    "Mexico", 
                    "Netherlands", 
                    "New Zealand", 
                    "Nigeria", 
                    "Norway", 
                    "Pakistan", 
                    "Peru", 
                    "Philippines", 
                    "Poland", 
                    "Portugal", 
                    "Romania", 
                    "Russia", 
                    "South Africa", 
                    "South Korea", 
                    "Soviet Union", 
                    "Spain", 
                    "Sweden", 
                    "Switzerland", 
                    "Taiwan", 
                    "Turkey", 
                    "UK", 
                    "USA", 
                    "Venezuela", 
                    "West Germany", 
                    "Yugoslavia", 
                    "Afghanistan", 
                    "Algeria", 
                    "Andorra", 
                    "Angola", 
                    "Antigua and Barbuda", 
                    "Armenia", 
                    "Aruba", 
                    "Azerbaijan", 
                    "Bahamas", 
                    "Bahrain", 
                    "Bangladesh", 
                    "Barbados", 
                    "Belarus", 
                    "Belize", 
                    "Benin", 
                    "Bermuda", 
                    "Bhutan", 
                    "Bolivia", 
                    "Bosnia and Herzegovina", 
                    "Botswana", 
                    "Burkina Faso", 
                    "Burma", 
                    "Burundi", 
                    "Cambodia", 
                    "Cameroon", 
                    "Cape Verde", 
                    "Central African Republic", 
                    "Chad", 
                    "Congo", 
                    "Costa Rica", 
                    "Cyprus", 
                    "Democratic Republic of Congo", 
                    "Djibouti", 
                    "Dominica", 
                    "Dominican Republic", 
                    "Ecuador", 
                    "El Salvador", 
                    "Eritrea", 
                    "Estonia", 
                    "Ethiopia", 
                    "Faroe Islands", 
                    "Federal Republic of Yugoslavia", 
                    "Fiji", 
                    "Gabon", 
                    "Ghana", 
                    "Gibraltar", 
                    "Greenland", 
                    "Guadeloupe", 
                    "Guatemala", 
                    "Guinea", 
                    "Guinea-Bissau", 
                    "Guyana", 
                    "Haiti", 
                    "Honduras", 
                    "Iceland", 
                    "Iraq", 
                    "Ivory Coast", 
                    "Jamaica", 
                    "Jordan", 
                    "Kazakhstan", 
                    "Kenya", 
                    "Kiribati", 
                    "Korea", 
                    "Kuwait", 
                    "Kyrgyzstan", 
                    "Laos", 
                    "Latvia", 
                    "Lebanon", 
                    "Lesotho", 
                    "Liberia", 
                    "Libya", 
                    "Liechtenstein", 
                    "Lithuania", 
                    "Luxembourg", 
                    "Macao", 
                    "Macau", 
                    "Madagascar", 
                    "Malaysia", 
                    "Mali", 
                    "Malta", 
                    "Martinique", 
                    "Mauritania", 
                    "Mauritius", 
                    "Moldova", 
                    "Monaco", 
                    "Mongolia", 
                    "Montenegro", 
                    "Morocco", 
                    "Mozambique", 
                    "Myanmar", 
                    "Namibia", 
                    "Nepal", 
                    "Netherlands Antilles", 
                    "Nicaragua", 
                    "Niger", 
                    "Niue", 
                    "North Korea", 
                    "North Vietnam", 
                    "Occupied Palestinian Territory", 
                    "Oman", 
                    "Palestine", 
                    "Panama", 
                    "Papua New Guinea", 
                    "Paraguay", 
                    "Puerto Rico", 
                    "Qatar", 
                    "Republic of Macedonia", 
                    "Rwanda", 
                    "Saint Vincent and the Grenadines", 
                    "San Marino", 
                    "Saudi Arabia", 
                    "Senegal", 
                    "Serbia", 
                    "Serbia and Montenegro", 
                    "Seychelles", 
                    "Siam", 
                    "Sierra Leone", 
                    "Singapore", 
                    "Slovakia", 
                    "Slovenia", 
                    "Somalia", 
                    "Sri Lanka", 
                    "Sudan", 
                    "Suriname", 
                    "Swaziland", 
                    "Syria", 
                    "Tajikistan", 
                    "Tanzania", 
                    "Thailand", 
                    "Togo", 
                    "Tonga", 
                    "Trinidad and Tobago", 
                    "Tunisia", 
                    "Turkmenistan", 
                    "U.S. Virgin Islands", 
                    "Uganda", 
                    "Ukraine", 
                    "United Arab Emirates", 
                    "United States Minor Outlying Islands", 
                    "Uruguay", 
                    "Uzbekistan", 
                    "Vietnam", 
                    "Western Sahara", 
                    "Yemen", 
                    "Zaire", 
                    "Zambia", 
                    "Zimbabwe"
                };

            this.CountryDictionary.Add(ScraperList.Imdb, this.imdb);
        }

        #endregion
    }
}