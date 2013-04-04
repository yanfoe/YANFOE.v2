// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ImageHandler.cs">
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
//   The image handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools
{
    #region Required Namespaces

    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Properties;
    using YANFOE.Settings;

    using Brushes = System.Drawing.Brushes;

    #endregion

    /// <summary>
    /// The image handler.
    /// </summary>
    public class ImageHandler
    {
        #region Public Methods and Operators

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="uniqueID">
        /// The unique id.
        /// </param>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Compare(Image value, string uniqueID, string s)
        {
            var path = Path.Combine(new[] { Get.FileSystemPaths.PathDirTemp, uniqueID + s });

            bool matchCase;

            using (var image = LoadImage(path))
            {
                matchCase = image == value;
            }

            return matchCase;
        }

        /// <summary>
        /// The load image.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static Image LoadImage(string filePath)
        {
            try
            {
                Image returnImage;

                if (!File.Exists(filePath))
                {
                    return Resources.picture;
                }

                var ms = new MemoryStream(File.ReadAllBytes(filePath));

                returnImage = Image.FromStream(ms);

                return returnImage;
            }
            catch (Exception)
            {
                return Resources.picture;
            }
        }

        /// <summary>
        /// The load image.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static Image LoadImage(string filePath, Size size)
        {
            return LoadImage(filePath, size.Width, size.Height);
        }

        /// <summary>
        /// The load image.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static Image LoadImage(string filePath, int width, int height)
        {
            if (!File.Exists(filePath))
            {
                return Resources.picture128;
            }

            var basePath = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar;
            var fileWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            var ext = Path.GetExtension(filePath);

            var thumbPath = basePath + fileWithoutExt + "." + width + "x" + height + ext;

            if (File.Exists(thumbPath))
            {
                return LoadImage(thumbPath);
            }

            Image resizedImage;

            using (var image = LoadImage(filePath))
            {
                resizedImage = ResizeImage(image, width, height);
                try
                {
                    resizedImage.Save(thumbPath);
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(LogSeverity.Error, 0, ex.Message, ex.StackTrace);
                }
            }

            if (resizedImage == null)
            {
                return Resources.picture128;
            }

            return resizedImage;
        }

        /// <summary>
        /// The load image source.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="ImageSource"/>.
        /// </returns>
        public static ImageSource LoadImageSource(string filePath)
        {
            try
            {
                BitmapImage bi = new BitmapImage(new Uri(filePath));
                return bi;
            }
            catch (Exception)
            {
                return null;

                // return Resources.picture.GetHbitmap();
            }
        }

        /// <summary>
        /// The load thumb.
        /// </summary>
        /// <param name="uniqueID">
        /// The unique id.
        /// </param>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static Image LoadThumb(string uniqueID, string s)
        {
            var path = Path.Combine(new[] { Get.FileSystemPaths.PathDirTemp, uniqueID + s });

            return !File.Exists(path) ? null : LoadImage(path);
        }

        /// <summary>
        /// The make thumbnail.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static Image MakeThumbnail(Image input, int width, int height)
        {
            var thumbNailImg = ResizeImage(input, width, height);
            var g = Graphics.FromImage(thumbNailImg);

            g.FillRectangle(Brushes.White, 0, 0, width, height);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(input, 0, 0, width, height);

            return thumbNailImg;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">
        /// The image to resize. 
        /// </param>
        /// <param name="width">
        /// The width to resize to. 
        /// </param>
        /// <param name="height">
        /// The height to resize to. 
        /// </param>
        /// <returns>
        /// The resized image. 
        /// </returns>
        public static Image ResizeImage(Image image, int width, int height)
        {
            var bitmap = new Bitmap(width, height);

            try
            {
                // use a graphics object to draw the resized image into the bitmap
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    // set the resize quality modes to high quality
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;

                    // draw the image into the target bitmap
                    graphics.DrawImage(image, 0, 0, width, height);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

            // return the resulting bitmap
            return bitmap;
        }

        /// <summary>
        /// The resize image.
        /// </summary>
        /// <param name="OriginalFile">
        /// The original file.
        /// </param>
        /// <param name="NewFile">
        /// The new file.
        /// </param>
        /// <param name="newWidth">
        /// The new width.
        /// </param>
        /// <param name="MaxHeight">
        /// The max height.
        /// </param>
        /// <param name="OnlyResizeIfWider">
        /// The only resize if wider.
        /// </param>
        /// <param name="quality">
        /// The quality.
        /// </param>
        public static void ResizeImage(
            string OriginalFile, string NewFile, int newWidth, int MaxHeight, bool OnlyResizeIfWider, int quality)
        {
            if (OriginalFile == string.Empty || !File.Exists(OriginalFile))
            {
                return;
            }

            using (var fullSizeImage = Image.FromFile(OriginalFile))
            {
                // Prevent using images internal thumbnail
                fullSizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                fullSizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

                if (OnlyResizeIfWider)
                {
                    if (fullSizeImage.Width <= newWidth)
                    {
                        newWidth = fullSizeImage.Width;
                    }
                }

                var newHeight = fullSizeImage.Height * newWidth / fullSizeImage.Width;

                if (newHeight > MaxHeight)
                {
                    // Resize with height instead
                    newWidth = fullSizeImage.Width * MaxHeight / fullSizeImage.Height;
                    newHeight = MaxHeight;
                }

                using (var newImage = fullSizeImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero))
                {
                    // Clear handle to original file so that we can overwrite it if necessary
                    fullSizeImage.Dispose();

                    var qualityParam = new EncoderParameter(Encoder.Quality, quality);

                    var jpegCodec = GetEncoderInfo("image/jpeg");
                    var encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = qualityParam;

                    // Save resized picture
                    newImage.Save(NewFile, jpegCodec, encoderParams);
                }
            }
        }

        /// <summary>
        /// The save thumb.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="uniqueID">
        /// The unique id.
        /// </param>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool SaveThumb(Image value, string uniqueID, string s)
        {
            try
            {
                if (value == null)
                {
                    return false;
                }

                var path = Path.Combine(new[] { Get.FileSystemPaths.PathDirTemp, uniqueID + s });

                value.Save(path);

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get encoder info.
        /// </summary>
        /// <param name="mimeType">
        /// The mime type.
        /// </param>
        /// <returns>
        /// The <see cref="ImageCodecInfo"/>.
        /// </returns>
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            var codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            return codecs.FirstOrDefault(t => t.MimeType == mimeType);
        }

        #endregion
    }
}