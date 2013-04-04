using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BitFactory.Logging;
using YANFOE.Factories;
using YANFOE.Factories.Import;
using YANFOE.Factories.Internal;
using YANFOE.InternalApps.Logs;
using YANFOE.Models.TvModels.Scan;
using YANFOE.Models.TvModels.Show;
using YANFOE.Models.TvModels.TVDB;
using YANFOE.Scrapers.TV;
using YANFOE.Tools.UI;
using Timer = System.Timers.Timer;

namespace YANFOE.UI.Dialogs.TV
{
    /// <summary>
    /// Interaction logic for WndImportTv.xaml
    /// </summary>
    public partial class WndImportTv : Window, INotifyPropertyChanged
    {
        private BackgroundWorker bgw;

        private TheTvdb theTvdb;

        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged("Count"); }
        }

        private int currentIndex = 0;

        private string currentStatus;
        public string CurrentStatus
        {
            get { return currentStatus; }
            set { currentStatus = value; OnPropertyChanged("CurrentStatus"); }
        }

        private Timer tmrUI;

        public WndImportTv()
        {
            InitializeComponent();


            this.bgw = new BackgroundWorker();
            ImportTvFactory.Instance.DoImportScan();

            this.btnOK_Click(null, null);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Count = 0;

            this.bgw = new BackgroundWorker();

            this.bgw.DoWork += this.Bgw_DoWork;
            this.bgw.WorkerReportsProgress = true;
            this.bgw.WorkerSupportsCancellation = true;
            this.bgw.RunWorkerCompleted += this.Bgw_RunWorkerCompleted;

            btnOK.IsEnabled = false;
            this.bgw.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the DoWork event of the Bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            string logCategory = "FrmImportTv > Bgw_DoWork";
            this.theTvdb = new TheTvdb();

            var toAdd = new Dictionary<string, ScanSeries>();
            var toRemove = new List<string>();

            Factories.UI.Windows7UIFactory.StartProgressState(ImportTvFactory.Instance.Scan.Count);

            var count = 0;

            InternalApps.Logs.Log.WriteToLog(LogSeverity.Debug, 0, string.Format("Starting import of tv series. {0} shows to scan.",
                    ImportTvFactory.Instance.Scan.Count), logCategory);

            foreach (var s in ImportTvFactory.Instance.Scan)
            {
                InternalApps.Logs.Log.WriteToLog(LogSeverity.Debug, 0, string.Format("Starting import of show {0}",
                    s.Key), logCategory);

                Factories.UI.Windows7UIFactory.SetProgressValue(count);

                var item = (from series in ImportTvFactory.Instance.SeriesList
                            where series.SeriesName == s.Key && series.WaitingForScan
                            select series).SingleOrDefault();

                if (item != null)
                {
                    if (!string.IsNullOrEmpty(s.Key))
                    {
                        this.CurrentStatus = "Processing " + s.Key;
                        InternalApps.Logs.Log.WriteToLog(LogSeverity.Info, 0, string.Format("Processing {0}",
                            s.Key), logCategory);

                        var searchResults = TVDBFactory.Instance.SearchDefaultShowDatabase(s.Key);

                        if (searchResults.Count == 0)
                        {
                            InternalApps.Logs.Log.WriteToLog(LogSeverity.Debug, 0, string.Format("{0} was not found in default show database. Trying TvDB",
                                s.Key), logCategory);
                            searchResults = this.theTvdb.SeriesSearch(Tools.Clean.Text.UrlEncode(s.Key)); // open initial object and do search
                        }

                        Series series;

                        if (searchResults.Count > 1 || searchResults.Count == 0)
                        {
                            InternalApps.Logs.Log.WriteToLog(LogSeverity.Debug, 0, string.Format("{0} results was found",
                                searchResults.Count), logCategory);

                            var scan =
                                 (from scanSeriesPick in ImportTvFactory.Instance.ScanSeriesPicks
                                  where scanSeriesPick.SearchString == s.Key
                                  select scanSeriesPick).SingleOrDefault();

                            if (scan != null)
                            {

                                InternalApps.Logs.Log.WriteToLog(LogSeverity.Debug, 0, string.Format("Attempting to search for {0} based on scanSeriesPick.",
                                    scan.SeriesName), logCategory);

                                searchResults = this.theTvdb.SeriesSearch(Tools.Clean.Text.UrlEncode(scan.SeriesName));

                                var result = (from r in searchResults where r.SeriesID == scan.SeriesID select r).Single();
                                series = this.theTvdb.OpenNewSeries(result);

                                InternalApps.Logs.Log.WriteToLog(LogSeverity.Info, 0, string.Format("{0} was found to have ID {1} (IMDb: {2})",
                                    series.SeriesName, series.ID, series.ImdbId), logCategory);
                            }
                            else
                            {
                                InternalApps.Logs.Log.WriteToLog(LogSeverity.Debug, 0, string.Format("{0} was not found in scanSeriesPick",
                                    s.Key), logCategory);

                                var resultCollection = new List<object>(4)
                                    { 
                                        searchResults, 
                                        s.Key, 
                                        s.Value, 
                                        toAdd, 
                                        toRemove 
                                    };

                                e.Result = resultCollection;

                                return;
                            }
                        }
                        else
                        {
                            series = this.theTvdb.OpenNewSeries(searchResults[0]); // download series details
                            InternalApps.Logs.Log.WriteToLog(LogSeverity.Debug, 0, string.Format("{0} was found to have ID {1} (IMDb: {2})",
                                    series.SeriesName, series.ID, series.ImdbId), logCategory);

                            if ((from scan in ImportTvFactory.Instance.ScanSeriesPicks where scan.SearchString == s.Key select s).Count() == 0)
                            {
                                ImportTvFactory.Instance.ScanSeriesPicks.Add(
                                    new ScanSeriesPick
                                    {
                                        SearchString = s.Key,
                                        SeriesID = series.SeriesID.ToString(),
                                        SeriesName = series.SeriesName
                                    });
                                InternalApps.Logs.Log.WriteToLog(LogSeverity.Debug, 0, string.Format("{0} was added to scanSeriesPick",
                                    series.SeriesName), logCategory);
                            }
                        }

                        this.Set(series, toRemove, toAdd, s.Key, s.Value);
                    }
                }

                this.Count++;

                this.bgw.ReportProgress(this.Count);

                if (this.bgw.CancellationPending)
                {
                    return;
                }

                count++;
                Thread.Sleep(2000);
            }

            foreach (var s in toRemove)
            {
                ImportTvFactory.Instance.Scan.Remove(s);
                InternalApps.Logs.Log.WriteToLog(LogSeverity.Debug, 0, string.Format("Removing {0} from scan database",
                    s), logCategory);
            }

            foreach (var s in toAdd)
            {
                ImportTvFactory.Instance.Scan.Add(s.Key, s.Value);
                InternalApps.Logs.Log.WriteToLog(LogSeverity.Debug, 0, string.Format("Adding {0} to scan database",
                    s), logCategory);
            }

            this.theTvdb.ApplyScan();

            Factories.UI.Windows7UIFactory.StopProgressState();
        }

        private void Set(Series series, List<string> toRemove, Dictionary<string, ScanSeries> toAdd, string key, ScanSeries value)
        {
            try
            {
                if (!TVDBFactory.Instance.TVDatabase.Any(x => x.SeriesName == series.SeriesName))
                {
                    TVDBFactory.Instance.TVDatabase.Add(series); // add series to db
                }

                var m = (from show in ImportTvFactory.Instance.SeriesList where show.SeriesName == key select show).Single();

                m.WaitingForScan = false;
                m.ScanComplete = true;

                var changedValue = value;
                toRemove.Add(key);

                if (!string.IsNullOrEmpty(series.SeriesName) && !toAdd.ContainsKey(series.SeriesName))
                {
                    toAdd.Add(series.SeriesName, changedValue);
                }
                else if (toAdd.ContainsKey(series.SeriesName))
                {
                    foreach (var season in changedValue.Seasons)
                    {
                        if (!toAdd[series.SeriesName].Seasons.ContainsKey(season.Key))
                        {
                            toAdd[series.SeriesName].Seasons.Add(season.Key, season.Value);
                        }
                        else
                        {
                            foreach (var episode in season.Value.Episodes)
                            {
                                toAdd[series.SeriesName].Seasons[season.Key].Episodes.Add(episode.Key, episode.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Log.WriteToLog(LogSeverity.Error, 0, "FrmImportTv > Set", exception.Message);
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the Bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                var resultObject = e.Result as List<object>;

                var s0 = resultObject[0] as ThreadedBindingList<SearchDetails>;
                var s1 = resultObject[1] as string;
                var s2 = resultObject[2] as ScanSeries;
                var toAdd = resultObject[3] as Dictionary<string, ScanSeries>;
                var toRemove = resultObject[4] as List<string>;

                var frmSelectSeries = new WndSelectSeries(s0, s1);
                frmSelectSeries.ShowDialog();

                if (frmSelectSeries.Cancelled)
                {
                    var seriesname =
                        (from s in ImportTvFactory.Instance.SeriesList where s.SeriesName == s1 select s).SingleOrDefault();

                    seriesname.Skipped = true;
                    seriesname.WaitingForScan = false;
                }
                else
                {

                    if (frmSelectSeries.SelectedSeries != null)
                    {
                        var check =
                            (from s in ImportTvFactory.Instance.ScanSeriesPicks where s.SearchString == s1 select s).Count() > 0;

                        if (!check)
                        {
                            ImportTvFactory.Instance.ScanSeriesPicks.Add(
                                new ScanSeriesPick
                                {
                                    SearchString = s1,
                                    SeriesID = frmSelectSeries.SelectedSeries.SeriesID,
                                    SeriesName = frmSelectSeries.SelectedSeries.SeriesName
                                });
                        }
                    }

                    var series = this.theTvdb.OpenNewSeries(frmSelectSeries.SelectedSeries);

                    this.Set(series, toRemove, toAdd, s1, s2);
                }

                this.btnOK_Click(null, null);
            }
            else
            {
                this.Hide();

                var frmNotCatagorized = new WndNotCategorized();
                frmNotCatagorized.ShowDialog();
            }

            DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.ScanSeriesPick);
            btnOK.IsEnabled = true;
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.bgw.CancelAsync();
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
