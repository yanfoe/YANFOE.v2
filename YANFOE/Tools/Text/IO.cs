// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="IO.cs">
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
//   The io.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Text
{
    #region Required Namespaces

    using System.IO;
    using System.Text;

    #endregion

    /// <summary>
    /// The io.
    /// </summary>
    public static class IO
    {
        #region Public Methods and Operators

        /// <summary>
        /// The read text from file.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ReadTextFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return string.Empty;
            }

            return File.ReadAllText(path);
        }

        /// <summary>
        /// The write text to file.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void WriteTextToFile(string path, string value)
        {
            var textWriter = new StreamWriter(path, false, Encoding.UTF8);

            try
            {
                textWriter.Write(value);
            }
            finally
            {
                textWriter.Flush();
                textWriter.Close();
            }
        }

        #endregion
    }
}