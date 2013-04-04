// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="FileSizeFormatProvider.cs">
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
//   The file size format provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   The file size format provider.
    /// </summary>
    public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
    {
        #region Constants

        /// <summary>
        ///   The file size format.
        /// </summary>
        private const string FileSizeFormat = "fs";

        /// <summary>
        ///   The one giga byte.
        /// </summary>
        private const decimal OneGigaByte = OneMegaByte * 1024M;

        /// <summary>
        ///   The one kilo byte.
        /// </summary>
        private const decimal OneKiloByte = 1024M;

        /// <summary>
        ///   The one mega byte.
        /// </summary>
        private const decimal OneMegaByte = OneKiloByte * 1024M;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The format.
        /// </summary>
        /// <param name="format">
        /// The format. 
        /// </param>
        /// <param name="arg">
        /// The arg. 
        /// </param>
        /// <param name="formatProvider">
        /// The format provider. 
        /// </param>
        /// <returns>
        /// The format. 
        /// </returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            decimal size;
            string suffix;

            if (format == null || !format.StartsWith(FileSizeFormat))
            {
                return DefaultFormat(format, arg, formatProvider);
            }

            if (arg is string)
            {
                return DefaultFormat(format, arg, formatProvider);
            }

            try
            {
                size = System.Convert.ToDecimal(arg);
            }
            catch (InvalidCastException)
            {
                return DefaultFormat(format, arg, formatProvider);
            }

            if (size > OneGigaByte)
            {
                size /= OneGigaByte;
                suffix = "GB";
            }
            else if (size > OneMegaByte)
            {
                size /= OneMegaByte;
                suffix = "MB";
            }
            else if (size > OneKiloByte)
            {
                size /= OneKiloByte;
                suffix = "kB";
            }
            else
            {
                suffix = " B";
            }

            string precision = format.Substring(2);

            if (string.IsNullOrEmpty(precision))
            {
                precision = "2";
            }

            return string.Format("{0:N" + precision + "}{1}", size, suffix);
        }

        /// <summary>
        /// The get format.
        /// </summary>
        /// <param name="formatType">
        /// The format type. 
        /// </param>
        /// <returns>
        /// The get format. 
        /// </returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The default format.
        /// </summary>
        /// <param name="format">
        /// The format. 
        /// </param>
        /// <param name="arg">
        /// The arg. 
        /// </param>
        /// <param name="formatProvider">
        /// The format provider. 
        /// </param>
        /// <returns>
        /// The default format. 
        /// </returns>
        private static string DefaultFormat(string format, object arg, IFormatProvider formatProvider)
        {
            var formattableArg = arg as IFormattable;

            if (formattableArg != null)
            {
                return formattableArg.ToString(format, formatProvider);
            }

            return arg.ToString();
        }

        #endregion
    }
}