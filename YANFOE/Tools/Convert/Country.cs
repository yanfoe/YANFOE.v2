// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Country.cs">
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
//   The country.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Convert
{
    #region Required Namespaces

    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.Tools.Enums;

    #endregion

    /// <summary>
    ///   The country.
    /// </summary>
    public static class Country
    {
        #region Public Methods and Operators

        /// <summary>
        /// Converts a country (full english name, 2 letter or 3 letter) to English country name.
        /// </summary>
        /// <param name="country">
        /// The country value 
        /// </param>
        /// <returns>
        /// The to full name. 
        /// </returns>
        public static string ToFullName(string country)
        {
            const string LogCatagory = "Convert > Country > ToFullName";

            try
            {
                CultureInfo name = FindCultureInfo(country);

                if (name == null)
                {
                    return string.Empty;
                }

                return name.EnglishName.ToLower(CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts a country (full english name, 2 letter or 3 letter) to a 2 or 3 letter ISO abbreiviation.
        /// </summary>
        /// <param name="country">
        /// The country value 
        /// </param>
        /// <param name="length">
        /// CountryAbbreviationLength length. 
        /// </param>
        /// <returns>
        /// The to short. 
        /// </returns>
        public static string ToShort(string country, CountryAbbreviationLength length)
        {
            const string LogCatagory = "Convert > Country > ToShort";

            try
            {
                CultureInfo name = FindCultureInfo(country);

                if (name == null)
                {
                    return string.Empty;
                }

                if (length == CountryAbbreviationLength.TwoLetter)
                {
                    return name.ThreeLetterISOLanguageName.ToLower(CultureInfo.CurrentCulture);
                }

                if (length == CountryAbbreviationLength.ThreeLetter)
                {
                    return name.TwoLetterISOLanguageName.ToLower(CultureInfo.CurrentCulture);
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
            }

            return string.Empty;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finds the culture info object related to a country value
        /// </summary>
        /// <param name="country">
        /// The country value 
        /// </param>
        /// <returns>
        /// Found culture 
        /// </returns>
        private static CultureInfo FindCultureInfo(string country)
        {
            const string LogCatagory = "Convert > Country > FindCultureInfo";

            try
            {
                foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
                {
                    Match match = Regex.Match(country, @".*?\s(\x28.*?\x29)");
                    if (match.Success)
                    {
                        country = country.Replace(match.Groups[1].Value, string.Empty).Trim();
                    }

                    country = country.Trim().ToLower(CultureInfo.CurrentCulture);

                    if (country == ci.EnglishName.ToLower(CultureInfo.CurrentCulture)
                        || country == ci.ThreeLetterISOLanguageName.ToLower()
                        || country == ci.TwoLetterISOLanguageName.ToLower(CultureInfo.CurrentCulture))
                    {
                        return ci;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
            }

            return null;
        }

        #endregion
    }
}