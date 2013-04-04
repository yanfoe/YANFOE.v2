// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Tools.cs">
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
//   Image tools
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Images
{
    #region Required Namespaces

    using System;
    using System.Drawing;

    #endregion

    /// <summary>
    ///   Image tools
    /// </summary>
    public static class Tools
    {
        #region Public Methods and Operators

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="OriginalImage">
        /// The Original image. 
        /// </param>
        /// <param name="newWidth">
        /// The new width. 
        /// </param>
        /// <param name="newHeight">
        /// The new height. 
        /// </param>
        /// <returns>
        /// Resized image 
        /// </returns>
        public static Image ResizeImage(Image OriginalImage, int newWidth, int newHeight)
        {
            return OriginalImage == null
                       ? null
                       : OriginalImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
        }

        #endregion
    }
}