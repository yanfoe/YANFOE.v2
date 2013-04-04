// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VideoSource.cs" company="The YANFOE Project">
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
//   The video source.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Tools.Lists
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;

    using YANFOE.Settings;
    using YANFOE.Tools.Extentions;

    /// <summary>
    /// The video source.
    /// </summary>
    public static class VideoSource
    {
        #region Constants and Fields

        /// <summary>
        /// The list field
        /// </summary>
        private static BindingList<string> list;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="VideoSource"/> class. 
        /// </summary>
        static VideoSource()
        {
            PopulateList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets List.
        /// </summary>
        public static BindingList<string> List
        {
            get
            {
                return list;
            }

            set
            {
                list = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The populate list.
        /// </summary>
        private static void PopulateList()
        {
            Debug.Write(Get.Media.MediaSource.Count);

            list = (from source in Get.Media.MediaSource orderby source.Key select source.Key).ToBindingList();
        }

        #endregion
    }
}