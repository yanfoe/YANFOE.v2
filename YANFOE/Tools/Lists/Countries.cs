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
namespace YANFOE.Tools.Lists
{
    #region Required Namespaces

    using System.Collections.Generic;
    using System.Globalization;

    #endregion

    /// <summary>
    ///   The countries.
    /// </summary>
    public class Countries
    {
        #region Public Methods and Operators

        /// <summary>
        ///   Get counties.
        /// </summary>
        /// <returns> Country collection </returns>
        public static SortedDictionary<string, string> GetCounties()
        {
            var countryList = new SortedDictionary<string, string>();
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                RegionInfo ri;
                try
                {
                    ri = new RegionInfo(ci.Name);
                }
                catch
                {
                    // If a RegionInfo object could not be created we don't want to use the CultureInfo
                    // for the country list.
                    continue;
                }

                // Create new country dictionary entry.
                var newKeyValuePair = new KeyValuePair<string, string>(ri.EnglishName, ri.ThreeLetterISORegionName);

                // If the country is not alreayd in the countryList add it...
                if (!countryList.ContainsKey(ri.EnglishName))
                {
                    countryList.Add(newKeyValuePair.Key, newKeyValuePair.Value);
                }
            }

            return countryList;
        }

        #endregion
    }
}