// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="AssociatedFilesModel.cs">
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
//   Associated Files Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.GeneralModels.AssociatedFiles
{
    #region Required Namespaces

    using System;
    using System.Linq;

    using YANFOE.Tools.Models;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   Associated Files Model
    /// </summary>
    [Serializable]
    public class AssociatedFilesModel : ModelBase
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="AssociatedFilesModel" /> class.
        /// </summary>
        public AssociatedFilesModel()
        {
            this.Media = new ThreadedBindingList<MediaModel>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the media collection.
        /// </summary>
        /// <value> The media collection </value>
        public ThreadedBindingList<MediaModel> Media { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add to media collection.
        /// </summary>
        /// <param name="fileModel">
        /// The file model. 
        /// </param>
        /// <param name="order">
        /// The order. 
        /// </param>
        public void AddToMediaCollection(MediaPathFileModel fileModel, int order = 1)
        {
            this.Media.Add(new MediaModel { PathAndFilename = fileModel.PathAndFileName, Order = 1 });
        }

        /// <summary>
        ///   The files as list.
        /// </summary>
        /// <returns> The <see cref="string" /> . </returns>
        public string FilesAsList()
        {
            return string.Join(", ", (from f in this.Media select f.PathAndFilename).ToArray());
        }

        /// <summary>
        ///   Gets the media collection.
        /// </summary>
        /// <returns> Media collection </returns>
        public ThreadedBindingList<MediaModel> GetMediaCollection()
        {
            return this.Media ?? (this.Media = new ThreadedBindingList<MediaModel>());
        }

        #endregion
    }
}