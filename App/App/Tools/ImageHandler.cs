// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageHandler.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    public class ImageHandler
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
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

        public static Image LoadImage(string filePath)
        {
            try
            {
                var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var returnImage = Image.FromStream(fs);
                fs.Close();

                return returnImage;
            }
            catch (Exception)
            {
                return null;
            }


        }

        public static Image LoadImage(string filePath, int width, int height)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            var basePath = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar;
            var fileWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            var ext = Path.GetExtension(filePath);

            var thumbPath = basePath + fileWithoutExt + "." + width + "x" + height + ext;

            if (File.Exists(thumbPath))
            {
                return LoadImage(thumbPath);
            }
            else
            {
                var image = LoadImage(filePath);
                var resizedImage = ResizeImage(image, width, height);
                resizedImage.Save(thumbPath);
                return resizedImage;
            }
        }

        public static void ResizeImage(string OriginalFile, string NewFile, int newWidth, int MaxHeight, bool OnlyResizeIfWider, int quality)
        {
            if (OriginalFile == string.Empty || !File.Exists(OriginalFile))
            {
                return;
            }

            var fullSizeImage = Image.FromFile(OriginalFile);

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

            var newImage = fullSizeImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            fullSizeImage.Dispose();

            var qualityParam = new EncoderParameter(Encoder.Quality, quality);

            var jpegCodec = GetEncoderInfo("image/jpeg");
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            // Save resized picture
            newImage.Save(NewFile, jpegCodec, encoderParams);
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            var codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            return codecs.FirstOrDefault(t => t.MimeType == mimeType);
        }

        public static Image MakeThumbnail(Image input, int width, int height)
        {
            var thumbNailImg = ResizeImage(input, width, height);
            var g = Graphics.FromImage(thumbNailImg);

            g.FillRectangle(Brushes.White, 0, 0, width, height);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(input, 0, 0, width, height);

            return thumbNailImg;
        } 
    }
}
