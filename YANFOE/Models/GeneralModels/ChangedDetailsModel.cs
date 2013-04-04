// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ChangedDetailsModel.cs">
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
//   The changed details factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.GeneralModels
{
    /// <summary>
    ///   The changed details factory.
    /// </summary>
    public class ChangedDetailsFactory
    {
        /// <summary>
        ///   The changed details.
        /// </summary>
        public class ChangedDetails
        {
            #region Fields

            /// <summary>
            ///   The changed image.
            /// </summary>
            private bool changedImage;

            /// <summary>
            ///   The changed text.
            /// </summary>
            private bool changedText;

            #endregion

            #region Public Properties

            /// <summary>
            ///   Gets a value indicating whether Changed.
            /// </summary>
            public bool Changed { get; private set; }

            /// <summary>
            ///   Gets or sets a value indicating whether ChangedImage.
            /// </summary>
            public bool ChangedImage
            {
                get
                {
                    return this.changedImage;
                }

                set
                {
                    this.changedImage = value;
                    this.CheckChanged();
                }
            }

            /// <summary>
            ///   Gets or sets a value indicating whether ChangedText.
            /// </summary>
            public bool ChangedText
            {
                get
                {
                    return this.changedText;
                }

                set
                {
                    this.changedText = value;
                    this.CheckChanged();
                }
            }

            #endregion

            #region Methods

            /// <summary>
            ///   The check changed.
            /// </summary>
            private void CheckChanged()
            {
                if (this.changedText || this.changedImage)
                {
                    this.Changed = true;
                }
                else
                {
                    this.Changed = false;
                }
            }

            #endregion
        }
    }
}