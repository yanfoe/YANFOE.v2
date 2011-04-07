// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenerateOutput.cs" company="The YANFOE Project">
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

namespace YANFOE.IO
{
    using YANFOE.Models.MovieModels;
    using YANFOE.Settings;
    using YANFOE.Tools.Enums;

    /// <summary>
    /// The generate output.
    /// </summary>
    public static class GenerateOutput
    {
        #region Constants and Fields

        /// <summary>
        /// The yamj IO Handler
        /// </summary>
        private static readonly YAMJ yamj = new YAMJ();

        #endregion

        #region Public Methods

        /// <summary>
        /// Access current IO Handler
        /// </summary>
        /// <returns>
        /// The current IO handler
        /// </returns>
        public static dynamic AccessCurrentIOHandler()
        {
            switch (Get.InOutCollection.IoType)
            {
                case NFOType.YAMJ:
                    return yamj;
            }

            return null;
        }

        /// <summary>
        /// Generatoe Movie Output using current IO Handler
        /// </summary>
        /// <param name="movieModel">
        /// The movie model.
        /// </param>
        /// <returns>
        /// The output path
        /// </returns>
        public static string GenerateMovieOutput(MovieModel movieModel)
        {
            switch (Get.InOutCollection.IoType)
            {
                case NFOType.YAMJ:
                    return yamj.GenerateMovieOutput(movieModel);
            }

            return string.Empty;
        }

        #endregion
    }
}