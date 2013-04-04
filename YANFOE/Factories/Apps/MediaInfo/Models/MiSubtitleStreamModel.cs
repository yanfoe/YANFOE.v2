// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiSubtitleStreamModel.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   The MediaInfo Subtitle stream model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Factories.Apps.MediaInfo.Models
{
    using System;

    /// <summary>
    /// The MediaInfo Subtitle stream model.
    /// </summary>
     [Serializable]
    public class MiSubtitleStreamModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MiSubtitleStreamModel"/> class.
        /// </summary>
        public MiSubtitleStreamModel()
        {
            this.Format = string.Empty;
            this.CodecID = string.Empty;
            this.CodecIDInfo = string.Empty;
            this.Language = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets CodecID.
        /// </summary>
        public string CodecID { get; set; }

        /// <summary>
        /// Gets or sets CodecIDInfo.
        /// </summary>
        public string CodecIDInfo { get; set; }

        /// <summary>
        /// Gets or sets Format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets Language.
        /// </summary>
        public string Language { get; set; }

        #endregion
    }
}