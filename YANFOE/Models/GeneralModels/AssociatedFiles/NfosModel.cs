// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NfosModel.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.GeneralModels.AssociatedFiles
{
    using System;

    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;

    /// <summary>
    /// The nfos model.
    /// </summary>
    [Serializable]
    public class NfosModel : ModelBase
    {
        #region Constants and Fields

        /// <summary>
        /// The nfo path.
        /// </summary>
        private Uri nfoPath;

        /// <summary>
        /// The nfo type.
        /// </summary>
        private NFOType nfoType;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NfosModel"/> class.
        /// </summary>
        public NfosModel()
        {
            this.NfoType = new NFOType();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets NfoPath.
        /// </summary>
        public Uri NfoPath
        {
            get
            {
                return this.nfoPath;
            }

            set
            {
                this.nfoPath = value;
                this.OnPropertyChanged("NfoType");
            }
        }

        /// <summary>
        /// Gets or sets NfoType.
        /// </summary>
        public NFOType NfoType
        {
            get
            {
                return this.nfoType;
            }

            set
            {
                this.nfoType = value;
                this.OnPropertyChanged("NfoType");
            }
        }

        #endregion
    }
}