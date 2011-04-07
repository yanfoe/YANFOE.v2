// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tools.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.Images
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Image tools
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="origionalImage">The origional image.</param>
        /// <param name="newWidth">The new width.</param>
        /// <param name="newHeight">The new height.</param>
        /// <returns>Resized image</returns>
        public static Image ResizeImage(Image origionalImage, int newWidth, int newHeight)
        {
            return origionalImage == null ? null : origionalImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
        }
    }
}
