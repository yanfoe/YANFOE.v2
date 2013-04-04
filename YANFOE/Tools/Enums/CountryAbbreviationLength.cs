// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="CountryAbbreviationLength.cs">
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
//   Specify a country abbreviation length.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Enums
{
    /// <summary>
    ///   Specify a country abbreviation length.
    /// </summary>
    public enum CountryAbbreviationLength
    {
        /// <summary>
        ///   Use a two letter abbriviation. Ie, EN for English.
        /// </summary>
        TwoLetter, 

        /// <summary>
        ///   Use a two letter abbriviation. Ie, ENG for English.
        /// </summary>
        ThreeLetter
    }
}