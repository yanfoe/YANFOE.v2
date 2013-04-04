// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ImageConverter.cs">
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
//   The image converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.UI
{
    #region Required Namespaces

    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    #endregion

    /// <summary>
    ///   The image converter.
    /// </summary>
    [ValueConversion(typeof(Image), typeof(ImageSource))]
    internal class ImageConverter : IValueConverter
    {
        #region Public Methods and Operators

        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="targetType">
        /// The target type. 
        /// </param>
        /// <param name="parameter">
        /// The parameter. 
        /// </param>
        /// <param name="culture">
        /// The culture. 
        /// </param>
        /// <returns>
        /// The <see cref="object"/> . 
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // empty images are empty...
            if (value == null)
            {
                return null;
            }

            var image = (Image)value;

            // Winforms Image we want to get the WPF Image from...
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            var memoryStream = new MemoryStream();

            // Save to a memory stream...
            image.Save(memoryStream, ImageFormat.Bmp);

            // Rewind the stream...
            memoryStream.Seek(0, SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            return bitmap;
        }

        /// <summary>
        /// The convert back.
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="targetType">
        /// The target type. 
        /// </param>
        /// <param name="parameter">
        /// The parameter. 
        /// </param>
        /// <param name="culture">
        /// The culture. 
        /// </param>
        /// <returns>
        /// The <see cref="object"/> . 
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}