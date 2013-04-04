// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="GenerateRandomID.cs">
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
//   The generate random id.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools
{
    #region Required Namespaces

    using System;
    using System.Linq;

    #endregion

    /// <summary>
    /// The generate random id.
    /// </summary>
    public static class GenerateRandomID
    {
        #region Public Methods and Operators

        /// <summary>
        /// The generate.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Generate()
        {
            long i = Guid.NewGuid().ToByteArray().Aggregate<byte, long>(1, (current, b) => current * (b + 1));
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        #endregion
    }
}