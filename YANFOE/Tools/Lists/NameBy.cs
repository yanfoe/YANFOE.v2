// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="NameBy.cs">
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
//   The name by.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Lists
{
    #region Required Namespaces

    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///   The name by.
    /// </summary>
    public static class NameBy
    {
        #region Static Fields

        /// <summary>
        ///   The name by selections.
        /// </summary>
        private static readonly Dictionary<int, string> nameBySelections = new Dictionary<int, string>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes static members of the <see cref="NameBy" /> class.
        /// </summary>
        static NameBy()
        {
            nameBySelections.Add(0, "Name By Folder");
            nameBySelections.Add(1, "Name By File");
        }

        #endregion

        #region Enums

        /// <summary>
        ///   The name by list.
        /// </summary>
        public enum NameByList
        {
            /// <summary>
            ///   Name by folder.
            /// </summary>
            Folder = 0, 

            /// <summary>
            ///   Name by file.
            /// </summary>
            File = 1
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets NameBySelections.
        /// </summary>
        public static Dictionary<int, string> NameBySelections
        {
            get
            {
                return nameBySelections;
            }
        }

        #endregion
    }
}