// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="NFOModel.cs">
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
//   The nfos model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.GeneralModels.AssociatedFiles
{
    #region Required Namespaces

    using System;

    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    ///   The NFO model.
    /// </summary>
    [Serializable]
    public class NFOModel : ModelBase
    {
        #region Fields

        /// <summary>
        ///   The NFO path.
        /// </summary>
        private Uri nfoPath;

        /// <summary>
        ///   The NFO type.
        /// </summary>
        private NFOType nfoType;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="NFOModel" /> class.
        /// </summary>
        public NFOModel()
        {
            this.NFOType = new NFOType();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets NFOPath.
        /// </summary>
        public Uri NFOPath
        {
            get
            {
                return this.nfoPath;
            }

            set
            {
                this.nfoPath = value;
                this.OnPropertyChanged("NFOType");
            }
        }

        /// <summary>
        ///   Gets or sets NFOType.
        /// </summary>
        public NFOType NFOType
        {
            get
            {
                return this.nfoType;
            }

            set
            {
                this.nfoType = value;
                this.OnPropertyChanged("NFOType");
            }
        }

        #endregion
    }
}