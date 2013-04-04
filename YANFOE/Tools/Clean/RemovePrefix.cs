// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="RemovePrefix.cs">
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
//   Remove prefixes from title
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Clean
{
    #region Required Namespaces

    using System.Collections.Generic;
    using System.Globalization;

    #endregion

    /// <summary>
    ///   Remove prefixes from title
    /// </summary>
    public static class RemovePrefix
    {
        #region Public Methods and Operators

        /// <summary>
        /// Moves a prefix from the start of a title to the end.
        /// </summary>
        /// <param name="title">
        /// The title. 
        /// </param>
        /// <returns>
        /// The fixes title 
        /// </returns>
        public static string Go(string title)
        {
            string[] prefixes = { "A", "An", "The", "Le", "The", "La", "Les", "Un", "Une", "De", "Het", "Een" };

            title = MoveFromStartToEnd(title, prefixes, true);
            return title;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Moves a value from start to end of a string
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="prefixes">
        /// The prefixes. 
        /// </param>
        /// <param name="withComma">
        /// if set to <c>true</c> [with comma]. 
        /// </param>
        /// <returns>
        /// Fixed string 
        /// </returns>
        private static string MoveFromStartToEnd(string value, IEnumerable<string> prefixes, bool withComma)
        {
            foreach (var s in prefixes)
            {
                if (value.StartsWith(s + " ", true, CultureInfo.CurrentCulture))
                {
                    value = value.Substring(s.Length + 1, value.Length - (s.Length + 1));

                    if (withComma)
                    {
                        value += ", " + s;
                    }
                }
            }

            return value;
        }

        #endregion
    }
}