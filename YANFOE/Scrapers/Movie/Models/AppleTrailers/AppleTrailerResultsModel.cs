// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="AppleTrailerResultsModel.cs">
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
//   The apple trailer results model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Scrapers.Movie.Models.AppleTrailers
{
    #region Required Namespaces

    using System;

    using Newtonsoft.Json;

    using YANFOE.Tools.Error;
    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    /// The apple trailer results model.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    [Serializable]
    public class AppleTrailerResultsModel : ModelBase, IMyDataErrorInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// Gets or sets the moviesite.
        /// </summary>
        public string moviesite { get; set; }

        /// <summary>
        /// Gets or sets the poster.
        /// </summary>
        public string poster { get; set; }

        /// <summary>
        /// Gets or sets the releasedate.
        /// </summary>
        public string releasedate { get; set; }

        /// <summary>
        /// Gets or sets the studio.
        /// </summary>
        public string studio { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string title { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// When implemented by a class, this method returns information on an error associated with a business object.
        /// </summary>
        /// <param name="info">
        /// An <see cref="T:DevExpress.XtraEditors.DXErrorProvider.ErrorInfo"/> object that contains information on an error. 
        /// </param>
        public void GetError(ErrorInfo info)
        {
        }

        /// <summary>
        /// When implemented by a class, this method returns information on an error associated with a specific business object's property.
        /// </summary>
        /// <param name="propertyName">
        /// A string that identifies the name of the property for which information on an error is to be returned. 
        /// </param>
        /// <param name="info">
        /// An <see cref="T:DevExpress.XtraEditors.DXErrorProvider.ErrorInfo"/> object that contains information on an error. 
        /// </param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
        }

        #endregion
    }
}