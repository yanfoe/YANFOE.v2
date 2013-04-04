// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddCustomSeriesModel.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Factories.AddCustomSeries
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using DevExpress.XtraEditors.DXErrorProvider;

    using YANFOE.Tools.Models;

    /// <summary>
    /// The add custom series model.
    /// </summary>
    public class AddCustomSeriesModel : ModelBase, IDXDataErrorInfo
    {
        #region Constants and Fields

        /// <summary>
        /// The add series zero.
        /// </summary>
        private bool addSeriesZero;

        /// <summary>
        /// The series.
        /// </summary>
        private int series;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCustomSeriesModel"/> class.
        /// </summary>
        public AddCustomSeriesModel()
        {
            this.SeriesList = new BindingList<string>();
            this.NewSeriesName = string.Empty;
            this.startAt = 1;
            this.Files = new Dictionary<int, BindingList<AddCustomSeriesFilesModel>>();
        }

        #endregion

        #region Properties

        public Dictionary<int, BindingList<AddCustomSeriesFilesModel>> Files { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add series zero].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add series zero]; otherwise, <c>false</c>.
        /// </value>
        public bool AddSeriesZero
        {
            get
            {
                return this.addSeriesZero;
            }

            set
            {
                this.addSeriesZero = value;
                this.startAt = this.addSeriesZero ? 0 : 1;
                this.GenerateSeriesList();
            }
        }

        private string newSeriesName;

        /// <summary>
        /// Gets or sets the new name of the series.
        /// </summary>
        /// <value>
        /// The new name of the series.
        /// </value>
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
        /// Gets or sets SelectedSeries.
        /// </summary>
        public int SelectedSeries { get; set; }

        /// <summary>
        /// Gets or sets Series.
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
        /// Gets or sets the series list.
        /// </summary>
        /// <value>
        /// The series list.
        /// </value>
        public BindingList<string> SeriesList { get; set; }

        /// <summary>
        /// Gets or sets startAt.
        /// </summary>
        private int startAt { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Generates the series list.
        /// </summary>
        private void GenerateSeriesList()
        {
            this.SeriesList.Clear();

            for (int i = this.startAt; i < this.series + 1; i++)
            {
                this.SeriesList.Add(string.Format("Series {0}", i));
            }

            AddCustomSeriesFactory.InvokeUpdateSeriesList(new EventArgs());
        }

        #endregion

        /// <summary>
        /// When implemented by a class, this method returns information on an error associated with a specific business object's property.
        /// </summary>
        /// <param name="propertyName">A string that identifies the name of the property for which information on an error is to be returned.</param>
        /// <param name="info">An <see cref="T:DevExpress.XtraEditors.DXErrorProvider.ErrorInfo"/> object that contains information on an error.</param>
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

                    var check = from s in TvDBFactory.MasterSeriesNameList where s.SeriesName == this.NewSeriesName select s;

                    if (check.Count() > 0)
                    {
                        info.ErrorText += "Series name already exists";
                        info.ErrorType = ErrorType.Critical;
                    }

                    break;
            }
        }

        /// <summary>
        /// When implemented by a class, this method returns information on an error associated with a business object.
        /// </summary>
        /// <param name="info">An <see cref="T:DevExpress.XtraEditors.DXErrorProvider.ErrorInfo"/> object that contains information on an error.</param>
        public void GetError(ErrorInfo info)
        {
        }
    }
}