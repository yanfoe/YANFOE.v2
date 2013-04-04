// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Maths.cs">
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
//   The maths.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools
{
    /// <summary>
    /// The maths.
    /// </summary>
    public static class Maths
    {
        #region Public Methods and Operators

        /// <summary>
        /// The process double.
        /// </summary>
        /// <param name="doubleValue">
        /// The double value.
        /// </param>
        /// <param name="multiply">
        /// The multiply.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ProcessDouble(string doubleValue, double multiply)
        {
            var ratingval = RemoveCommasAndDots(doubleValue);

            var p1 = ratingval.Substring(0, 1);

            string p2;

            if (ratingval.Length == 1)
            {
                p2 = "0";
            }
            else
            {
                p2 = ratingval.Substring(1, 1);
            }

            var p1s = double.Parse(p1) * multiply;
            var p2s = (double.Parse(p2) * multiply) / 10;

            var p3 = (p1s + p2s).ToString();

            if (p3.Length == 1)
            {
                p1 = p3;
                p2 = "0";
            }
            else
            {
                p3 = RemoveCommasAndDots(p3);

                p1 = p3.Substring(0, 1);
                p2 = p3.Substring(1, 1);
            }

            return string.Format("{0}.{1}", p1, p2);
        }

        /// <summary>
        /// The process multiple.
        /// </summary>
        /// <param name="doubleValue">
        /// The double value.
        /// </param>
        /// <param name="devide">
        /// The devide.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ProcessMultiple(string doubleValue, int devide)
        {
            doubleValue = RemoveCommasAndDots(doubleValue);

            var full = double.Parse(doubleValue);

            var fullDevided = full / devide;

            return ProcessDouble(fullDevided.ToString(), 1);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The remove commas and dots.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string RemoveCommasAndDots(string value)
        {
            value = value.Replace(",", string.Empty).Replace(".", string.Empty);
            return value;
        }

        #endregion
    }
}