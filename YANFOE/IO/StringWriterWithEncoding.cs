// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="StringWriterWithEncoding.cs">
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
//   The string writer with encoding.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.IO
{
    #region Required Namespaces

    using System.IO;
    using System.Text;

    #endregion

    /// <summary>
    ///   The string writer with encoding.
    /// </summary>
    public sealed class StringWriterWithEncoding : StringWriter
    {
        #region Fields

        /// <summary>
        ///   The encoding.
        /// </summary>
        private readonly Encoding encoding;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
        /// </summary>
        /// <param name="encoding">
        /// The encoding. 
        /// </param>
        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets Encoding.
        /// </summary>
        public override Encoding Encoding
        {
            get
            {
                return this.encoding;
            }
        }

        #endregion
    }
}