// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="PersonModel.cs">
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
//   The Person model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Models
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   The Person model.
    /// </summary>
    [Serializable]
    public class PersonModel : ModelBase
    {
        #region Fields

        /// <summary>
        ///   The name.
        /// </summary>
        private string name;

        /// <summary>
        ///   The unique id.
        /// </summary>
        private string uniqueId;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        /// <param name="name">
        /// The persons name. 
        /// </param>
        /// <param name="imageUrl">
        /// The image URL. 
        /// </param>
        /// <param name="role">
        /// The persons role. 
        /// </param>
        /// <param name="save">
        /// if set to <c>true</c> [save]. 
        /// </param>
        /// <param name="thumbPreUrl">
        /// The thumb Pre Url. 
        /// </param>
        public PersonModel(
            string name, string imageUrl = null, string role = null, bool save = false, string thumbPreUrl = null)
        {
            this.UniqueID = Guid.NewGuid().ToString();

            if (role == null)
            {
                role = string.Empty;
            }

            if (imageUrl == null)
            {
                imageUrl = string.Empty;
            }

            this.Name = name;
            this.Role = role;
            this.ImageUrl = imageUrl;

            if (!string.IsNullOrEmpty(thumbPreUrl))
            {
                this.ImageUrl = thumbPreUrl + this.ImageUrl;
            }

            this.Save = save;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the image URL.
        /// </summary>
        /// <value> The image URL. </value>
        public string ImageUrl { get; set; }

        /// <summary>
        ///   Gets or sets the persons name.
        /// </summary>
        /// <value> The name of the person </value>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        /// <summary>
        ///   Gets or sets the persons role.
        /// </summary>
        /// <value> The role of the person </value>
        public string Role { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref="PersonModel" /> is top be saved.
        /// </summary>
        /// <value> <c>true</c> if save; otherwise, <c>false</c> . </value>
        public bool Save { get; set; }

        /// <summary>
        ///   Gets or sets UniqueID.
        /// </summary>
        public string UniqueID
        {
            get
            {
                return this.uniqueId;
            }

            set
            {
                this.uniqueId = value;
            }
        }

        #endregion
    }
}