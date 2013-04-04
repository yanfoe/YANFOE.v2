// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MiSubtitleStreamModel.cs">
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
//   The MediaInfo Subtitle stream model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Apps.MediaInfo.Models
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   The MediaInfo Subtitle stream model.
    /// </summary>
    [Serializable]
    public class MiSubtitleStreamModel
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="MiSubtitleStreamModel" /> class.
        /// </summary>
        public MiSubtitleStreamModel()
        {
            this.Format = string.Empty;
            this.CodecId = string.Empty;
            this.CodecIdInfo = string.Empty;
            this.Language = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets CodecID.
        /// </summary>
        public string CodecId { get; set; }

        /// <summary>
        ///   Gets or sets CodecIDInfo.
        /// </summary>
        public string CodecIdInfo { get; set; }

        /// <summary>
        ///   Gets or sets Format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        ///   Gets or sets ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///   Gets or sets Language.
        /// </summary>
        public string Language { get; set; }

        #endregion
    }
}