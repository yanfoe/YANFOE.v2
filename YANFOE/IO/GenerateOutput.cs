// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="GenerateOutput.cs">
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
//   The generate output.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.IO
{
    #region Required Namespaces

    using YANFOE.Models.MovieModels;
    using YANFOE.Settings;
    using YANFOE.Tools.Enums;

    #endregion

    /// <summary>
    ///   The generate output.
    /// </summary>
    public static class GenerateOutput
    {
        #region Static Fields

        /// <summary>
        ///   The XBMC IO Handler
        /// </summary>
        private static readonly XBMC Xbmc = new XBMC();

        /// <summary>
        ///   The YAMJ IO Handler
        /// </summary>
        private static readonly YAMJ Yamj = new YAMJ();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Access current IO Handler
        /// </summary>
        /// <returns> The current IO handler </returns>
        public static IOInterface AccessCurrentIOHandler()
        {
            switch (Get.InOutCollection.IoType)
            {
                case NFOType.YAMJ:
                    return Yamj;
                case NFOType.XBMC:
                    return Xbmc;
            }

            return null;
        }

        /// <summary>
        /// Generate Movie Output using current IO Handler
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
                    return Yamj.GenerateMovieOutput(movieModel);
                case NFOType.XBMC:
                    return Xbmc.GenerateMovieOutput(movieModel);
            }

            return string.Empty;
        }

        #endregion
    }
}