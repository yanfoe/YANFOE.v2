using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YANFOE.UI.Dialogs.TV
{
    using System.ComponentModel;

    using BitFactory.Logging;

    using YANFOE.Factories;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Scrapers.TV;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.UI;

    /// <summary>
    /// Interaction logic for WndUpdateShows.xaml
    /// </summary>
    public partial class WndUpdateShows : Window, INotifyPropertyChanged
    {
        #region Constants and Fields

        /// <summary>
        /// The bgw update.
        /// </summary>
        private readonly BackgroundWorker bgwUpdate = new BackgroundWorker();

        /// <summary>
        /// The bgw scan.
        /// </summary>
        private BackgroundWorker bgwScan = new BackgroundWorker();

        private string currentStatus;

        public string CurrentStatus
        {
            get
            {
                return this.currentStatus;
            }
            set
            {
                this.currentStatus = value;
                OnPropertyChanged("CurrentStatus");
            }
        }

        /// <summary>
        /// The update database.
        /// </summary>
        private ThreadedBindingList<UpdateTvRecords> updateDatabase;

        public ThreadedBindingList<UpdateTvRecords> UpdateDatabase
        {
            get
            {
                return this.updateDatabase;
            }
            set
            {
                this.updateDatabase = value;
                OnPropertyChanged("UpdateDatabase");
            }
        }

        #endregion

        public WndUpdateShows(bool autoStart = false)
        {
            this.InitializeComponent();

            this.GenerateUpdateDatabase();

            if (autoStart)
            {
                this.DoFullScan();
            }
        }

        public WndUpdateShows(List<Episode> episodes, bool autoStart = false)
        {
            this.InitializeComponent();

            if (this.UpdateDatabase == null)
            {
                this.UpdateDatabase = new ThreadedBindingList<UpdateTvRecords>();
            }

            InternalApps.Logs.Log.WriteToLog(
                        LogSeverity.Debug,
                        0,
                        "UI > Dialogs > TV > Update Episodes",
                        string.Format(
                            "Adding {0} episodes(s) {{1}} to update queue",
                            episodes[0].GetSeriesName(),
                            string.Join(", ", (from s in episodes select string.Format("{0}x{1:00}", s.SeasonNumber.GetValueOrDefault(0), s.EpisodeNumber.GetValueOrDefault(0))).ToList())
                        )
            );

            this.UpdateDatabase.Clear();
            foreach (var ep in episodes)
            {
                try
                {
                    this.UpdateDatabase.AddRange((from s in TVDBFactory.Instance.TVDatabase
                                                  where s.SeriesName == ep.GetSeriesName()
                                                  select
                                                      new UpdateTvRecords
                                                      {
                                                          SeriesName = s.SeriesName,
                                                          SeriesId = s.SeriesID,
                                                          PreviousTime = ep.Lastupdated,
                                                          SeasonNumber = ep.SeasonNumber,
                                                          EpisodeNumber = ep.EpisodeNumber
                                                      }).ToList());
                }
                catch (ArgumentNullException e)
                {
                    InternalApps.Logs.Log.WriteToLog(
                        LogSeverity.Error,
                        0,
                        "UI > Dialogs > TV > Update Episodes",
                        e.Message);
                }
            }

            if (autoStart)
            {
                this.DoFullScan();
            }
        }

        public WndUpdateShows(List<Season> seasons, bool autoStart = false)
        {
            this.InitializeComponent();

            if (this.UpdateDatabase == null)
            {
                this.UpdateDatabase = new ThreadedBindingList<UpdateTvRecords>();
            }

            InternalApps.Logs.Log.WriteToLog(
                        LogSeverity.Debug,
                        0,
                        "UI > Dialogs > TV > Update Shows",
                        string.Format(
                            "Adding {0} season(s) {{1}} to update queue",
                            seasons[0].GetSeries().SeriesName,
                            string.Join(", ", (from s in seasons select s.SeasonNumber).ToList())
                        )
            );

            this.UpdateDatabase.Clear();
            foreach (var season in seasons)
            {
                try
                {
                    this.UpdateDatabase.AddRange((from s in TVDBFactory.Instance.TVDatabase
                                                  where s.SeriesName == season.GetSeries().SeriesName
                                                  select
                                                      new UpdateTvRecords
                                                      {
                                                          SeriesName = s.SeriesName,
                                                          SeriesId = s.SeriesID,
                                                          PreviousTime = season.Episodes[0].Lastupdated,
                                                          SeasonNumber = season.SeasonNumber
                                                      }).ToList());
                }
                catch (ArgumentNullException e)
                {
                    InternalApps.Logs.Log.WriteToLog(
                        LogSeverity.Error,
                        0,
                        "UI > Dialogs > TV > Update Shows",
                        e.Message);
                }
            }

            if (autoStart)
            {
                this.DoFullScan();
            }
        }

        private void DoFullScan()
        {
            this.btnOK.IsEnabled = false;

            this.bgwScan = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };

            this.bgwScan.DoWork += this.BgwScan_DoWork;
            this.bgwScan.ProgressChanged += this.BgwScan_ProgressChanged;
            this.bgwScan.RunWorkerCompleted += this.BgwScan_RunWorkerCompleted;

            this.bgwScan.RunWorkerAsync();
        }

        /// <summary>
        /// Generates the UpdateDatabase
        /// </summary>
        private void GenerateUpdateDatabase()
        {
            if (this.UpdateDatabase == null)
            {
                this.UpdateDatabase = new ThreadedBindingList<UpdateTvRecords>();
            }

            this.UpdateDatabase.Clear();

            foreach (UpdateTvRecords updateTvRecord in
                TVDBFactory.Instance.TVDatabase.OrderBy(s => s.SeriesName).Select(
                    s =>
                    new UpdateTvRecords
                        {
                            SeriesName = s.SeriesName, SeriesId = s.SeriesID, PreviousTime = s.Lastupdated 
                        }))
            {
                this.UpdateDatabase.Add(updateTvRecord);
            }
        }

        /// <summary>
        /// Handles the DoWork event of the bgwScan control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private void BgwScan_DoWork(object sender, DoWorkEventArgs e)
        {
            var tvdb = new TheTvdb();
            int count = 0;

            foreach (UpdateTvRecords record in this.UpdateDatabase)
            {
                InternalApps.Logs.Log.WriteToLog(
                            LogSeverity.Debug,
                            0,
                            "UI > Dialogs > TV > FrmUpdateShows > BgwScan_DoWork",
                            string.Format(
                                "Checking {0} {1}x{2:00} for update",
                                record.SeriesName, record.SeasonNumber.GetValueOrDefault(0), record.EpisodeNumber.GetValueOrDefault(0)
                            )
                );

                this.bgwScan.ReportProgress(0, record);

                Series seriesObj = TVDBFactory.Instance.GetSeriesFromName(record.SeriesName);

                Series newSeries = tvdb.CheckForUpdate(seriesObj.SeriesID, seriesObj.Language, seriesObj.Lastupdated);

                if (newSeries != null)
                {
                    InternalApps.Logs.Log.WriteToLog(
                                LogSeverity.Debug,
                                0,
                                "UI > Dialogs > TV > FrmUpdateShows > BgwScan_DoWork",
                                string.Format(
                                    "Update found for {0} {1}x{2:00}",
                                    record.SeriesName, record.SeasonNumber.GetValueOrDefault(0), record.EpisodeNumber.GetValueOrDefault(0)
                                )
                    );

                    record.NewTime = newSeries.Lastupdated;
                    count++;
                }
            }

            e.Result = count;
        }

        /// <summary>
        /// Handles the ProgressChanged event of the bgwScan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void BgwScan_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var record = e.UserState as UpdateTvRecords;
            CurrentStatus = string.Format("Updating: {0}", string.Format(
                "{0} {1}x{2:00}",
                record.SeriesName,
                record.SeasonNumber.GetValueOrDefault(0),
                record.EpisodeNumber.GetValueOrDefault(0)
                )
            );

            UpdateTvRecords row;
            if (record.EpisodeNumber != null)
            {
                row =
                    (from r in this.UpdateDatabase where r.SeriesName == record.SeriesName && r.SeasonNumber == record.SeasonNumber && r.EpisodeNumber == record.EpisodeNumber select r).SingleOrDefault();
            }
            else if (record.SeasonNumber != null)
            {
                row =
                    (from r in this.UpdateDatabase where r.SeriesName == record.SeriesName && r.SeasonNumber == record.SeasonNumber select r).SingleOrDefault();
            }
            else
            {
                row =
                    (from r in this.UpdateDatabase where r.SeriesName == record.SeriesName select r).SingleOrDefault();
            }

            int index = this.UpdateDatabase.IndexOf(row);
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwScan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BgwScan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnOK.IsEnabled = true;
            CurrentStatus = string.Format("Updating Complete. {0} updated elements available.", (int)e.Result);
        }

        /// <summary>
        /// Handles the DoWork event of the bgwUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void BgwUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (UpdateTvRecords record in this.UpdateDatabase)
            {
                if (record.UpdateSeries)
                {
                    if (record.EpisodeNumber != null)
                    {
                        this.bgwUpdate.ReportProgress(0, string.Format("{0} episode {1}x{1:00}", record.SeriesName, record.SeasonNumber, record.EpisodeNumber));
                    }
                    else if (record.SeasonNumber != null)
                    {
                        this.bgwUpdate.ReportProgress(0, string.Format("{0} season {1}", record.SeriesName, record.SeasonNumber));
                    }
                    else
                    {
                        this.bgwUpdate.ReportProgress(0, record.SeriesName);
                    }
                    TVDBFactory.Instance.UpdateSeries(record.SeriesId, record.SeasonNumber, record.EpisodeNumber);
                }
            }
        }

        /// <summary>
        /// Handles the ProgressChanged event of the bgwUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void BgwUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentStatus = string.Format("Updating {0}", e.UserState);
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BgwUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.bgwUpdate.DoWork += this.BgwUpdate_DoWork;
            this.bgwUpdate.ProgressChanged += this.BgwUpdate_ProgressChanged;
            this.bgwUpdate.RunWorkerCompleted += this.BgwUpdate_RunWorkerCompleted;

            this.bgwUpdate.WorkerReportsProgress = true;

            this.bgwUpdate.RunWorkerAsync();

            this.btnOK.IsEnabled = false;
            this.btnCancel.IsEnabled = false;
        }

        /// <summary>
        /// The update tv records.
        /// </summary>
        public class UpdateTvRecords : ModelBase
        {
            #region Constants and Fields

            /// <summary>
            /// The new time.
            /// </summary>
            private string newTime;

            #endregion

            #region Properties

            /// <summary>
            /// Gets or sets NewTime.
            /// </summary>
            public string NewTime
            {
                set
                {
                    this.newTime = value;

                    this.UpdatedOnServer = this.newTime != this.PreviousTime;
                    this.UpdateSeries = this.newTime != this.PreviousTime;
                }
            }

            /// <summary>
            /// Gets or sets PreviousTime.
            /// </summary>
            public string PreviousTime { private get; set; }

            /// <summary>
            /// Gets or sets SeriesId.
            /// </summary>
            public uint? SeriesId { get; set; }

            /// <summary>
            /// Gets or sets SeriesName.
            /// </summary>
            public string SeriesName { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [update series].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [update series]; otherwise, <c>false</c>.
            /// </value>
            public bool UpdateSeries { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether UpdatedOnServer.
            /// </summary>
            public bool UpdatedOnServer { get; set; }

            public int? SeasonNumber { get; set; }
            public int? EpisodeNumber { get; set; }

            #endregion
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
