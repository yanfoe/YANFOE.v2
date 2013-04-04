// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="AddCustomSeriesModel.cs">
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
//   The add custom series model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.AddCustomSeries
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using YANFOE.Tools.Error;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   The add custom series model.
    /// </summary>
    public class AddCustomSeriesModel : ModelBase, IMyDataErrorInfo
    {
        #region Fields

        /// <summary>
        ///   The add series zero.
        /// </summary>
        private bool addSeriesZero;

        /// <summary>
        ///   The new series name.
        /// </summary>
        private string newSeriesName;

        /// <summary>
        ///   The series.
        /// </summary>
        private int series;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="AddCustomSeriesModel" /> class.
        /// </summary>
        public AddCustomSeriesModel()
        {
            this.SeriesList = new ThreadedBindingList<string>();
            this.NewSeriesName = string.Empty;
            this.StartAt = 1;
            this.Files = new Dictionary<int, ThreadedBindingList<AddCustomSeriesFilesModel>>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets a value indicating whether [add series zero].
        /// </summary>
        /// <value> <c>true</c> if [add series zero]; otherwise, <c>false</c> . </value>
        public bool AddSeriesZero
        {
            get
            {
                return this.addSeriesZero;
            }

            set
            {
                this.addSeriesZero = value;
                this.StartAt = this.addSeriesZero ? 0 : 1;
                this.GenerateSeriesList();
            }
        }

        /// <summary>
        ///   Gets or sets the files.
        /// </summary>
        public Dictionary<int, ThreadedBindingList<AddCustomSeriesFilesModel>> Files { get; set; }

        /// <summary>
        ///   Gets or sets the new name of the series.
        /// </summary>
        /// <value> The new name of the series. </value>
        public string NewSeriesName
        {
            get
            {
                return this.newSeriesName;
            }

            set
            {
                this.newSeriesName = value;
                this.OnPropertyChanged("NewSeriesName");
            }
        }

        /// <summary>
        ///   Gets or sets SelectedSeries.
        /// </summary>
        public int SelectedSeries { get; set; }

        /// <summary>
        ///   Gets or sets Series.
        /// </summary>
        public int Series
        {
            get
            {
                return this.series;
            }

            set
            {
                if (this.series != value)
                {
                    this.series = value;
                    this.GenerateSeriesList();
                }
            }
        }

        /// <summary>
        ///   Gets or sets the series list.
        /// </summary>
        /// <value> The series list. </value>
        public ThreadedBindingList<string> SeriesList { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets startAt.
        /// </summary>
        private int StartAt { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// When implemented by a class, this method returns information on an error associated with a business object.
        /// </summary>
        /// <param name="info">
        /// An <see cref="ErrorInfo"/> object that contains information on an error. 
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
        /// An <see cref="ErrorInfo"/> object that contains information on an error. 
        /// </param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            switch (propertyName)
            {
                case "NewSeriesName":
                    if (this.NewSeriesName == string.Empty)
                    {
                        info.ErrorText += "New series name must not be blank.";
                        info.ErrorType = ErrorType.Critical;
                    }

                    var check = from s in TVDBFactory.Instance.MasterSeriesList
                                where s.SeriesName == this.NewSeriesName
                                select s;

                    if (check.Any())
                    {
                        info.ErrorText += "Series name already exists";
                        info.ErrorType = ErrorType.Critical;
                    }

                    break;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Generates the series list.
        /// </summary>
        private void GenerateSeriesList()
        {
            this.SeriesList.Clear();

            for (int i = this.StartAt; i < this.series + 1; i++)
            {
                this.SeriesList.Add(string.Format("Series {0}", i));
            }

            AddCustomSeriesFactory.InvokeUpdateSeriesList(new EventArgs());
        }

        #endregion
    }
}