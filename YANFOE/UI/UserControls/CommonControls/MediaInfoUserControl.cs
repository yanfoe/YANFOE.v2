namespace YANFOE.UI.UserControls.CommonControls
{
    using System;
    using System.Windows.Forms;

    using YANFOE.Factories;
    using YANFOE.Factories.Apps.MediaInfo.Models;
    using YANFOE.Models.NFOModels;

    public partial class MediaInfoUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public enum FileInfoType
        {
            Movie,
            TV
        }

        private FileInfoType _type;

        public FileInfoType Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
                InitialSetup();
                PopulateDropdowns();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaInfoUserControl"/> class.
        /// </summary>
        public MediaInfoUserControl()
        {
            InitializeComponent();
        }

        private void PopulateFileInfo()
        {
            FileInfoModel fileInfoModel;

            fileInfoModel = this.Type == FileInfoType.Movie ? MovieDBFactory.GetCurrentMovie().FileInfo : TvDBFactory.CurrentEpisode.FileInfo;

            grdAudioStreams.DataSource = fileInfoModel.AudioStreams;
            grdSubtitleStreams.DataSource = fileInfoModel.SubtitleStreams;

            txtVideoCodec.DataBindings.Clear();
            txtVideoCodec.DataBindings.Add("Text", fileInfoModel, "Codec", true, DataSourceUpdateMode.OnPropertyChanged);
            txtVideoCodec.Refresh();

            txtWidth.DataBindings.Clear();
            txtWidth.DataBindings.Add("Text", fileInfoModel, "Width", true, DataSourceUpdateMode.OnPropertyChanged);
            txtWidth.Refresh();

            txtHeight.DataBindings.Clear();
            txtHeight.DataBindings.Add("Text", fileInfoModel, "Height", true, DataSourceUpdateMode.OnPropertyChanged);
            txtHeight.Refresh();

            txtFPSFull.DataBindings.Clear();
            txtFPSFull.DataBindings.Add("Text", fileInfoModel, "FPS");
            txtFPSFull.Refresh();

            txtFPSRounded.DataBindings.Clear();
            txtFPSRounded.DataBindings.Add("Text", fileInfoModel, "FPSRounded");
            txtFPSRounded.Refresh();

            cmbAspectRatioDecimal.DataBindings.Clear();
            cmbAspectRatioDecimal.DataBindings.Add("Text", fileInfoModel, "AspectRatioDecimal");
            cmbAspectRatioDecimal.Refresh();

            cmbAspectRatio.DataBindings.Clear();
            cmbAspectRatio.DataBindings.Add("Text", fileInfoModel, "AspectRatio");
            cmbAspectRatio.Refresh();

            txtResolution.DataBindings.Clear();
            txtResolution.DataBindings.Add("Text", fileInfoModel, "Resolution");
            txtResolution.Refresh();

            chkInterlaced.DataBindings.Clear();
            chkInterlaced.DataBindings.Add("Checked", fileInfoModel, "InterlacedScan");

            chkProgressive.DataBindings.Clear();
            chkProgressive.DataBindings.Add("Checked", fileInfoModel, "ProgressiveScan");

            chkPal.DataBindings.Clear();
            chkPal.DataBindings.Add("Checked", fileInfoModel, "Pal");

            chkNtsc.DataBindings.Clear();
            chkNtsc.DataBindings.Add("Checked", fileInfoModel, "Ntsc");

            if (this.Type == FileInfoType.Movie)
            {
                var currentMovie = MovieDBFactory.GetCurrentMovie();

                if (currentMovie.AssociatedFiles.Media.Count > 0 && currentMovie.AssociatedFiles.Media != null)
                {
                    xmlPreviewMediaInfoOutput.SetXML(currentMovie.AssociatedFiles.Media[0].ScanXML);
                }

                xmlPreview.SetXML(IO.GenerateOutput.AccessCurrentIOHandler().GetFileInfo(movie: currentMovie));
            }
            else
            {
                var currentEpisode = TvDBFactory.CurrentEpisode;

                xmlPreviewMediaInfoOutput.SetXML(currentEpisode.FilePath.MiResponseModel.ScanXML);

                xmlPreview.SetXML(IO.GenerateOutput.AccessCurrentIOHandler().GetFileInfo(episode: currentEpisode));
            }


        }

        private void PopulateDropdowns()
        {
            cmbAspectRatio.Properties.Items.AddRange(Settings.Get.MediaInfo.AspectRatio);
            cmbAspectRatioDecimal.Properties.Items.AddRange(Settings.Get.MediaInfo.AspectRatioDecimal);
        }

        private void InitialSetup()
        {
            if (this.Type == FileInfoType.Movie)
            {
                this.SetupMovieEventBinding();
            }
            else if (this.Type == FileInfoType.TV)
            {
                this.SetupTvEventBindings();
            }
        }

        private void SetupMovieEventBinding()
        {
            MovieDBFactory.CurrentMovieChanged += (sender, e) =>
                {
                    MovieDBFactory.GetCurrentMovie().MediaInfoChanged += (s, e2) =>
                        {

                            if (this.InvokeRequired)
                            {
                                this.Invoke(new EventHandler(delegate(object o, EventArgs a)
                                {
                                    this.PopulateFileInfo();
                                }));
                            }
                            else
                            {
                                this.PopulateFileInfo();
                            }
                            
                        };

                    this.RefreshMovieBindings();
                };
        }

        private void SetupTvEventBindings()
        {
            TvDBFactory.CurrentEpisodeChanged += (sender, e) =>
                {
                    TvDBFactory.CurrentEpisode.MediaInfoChanged += (s, e2) =>
                        {
                            this.PopulateFileInfo();
                        };

                    this.RefreshTvBindings();
                    
                };
        }

        private void RefreshMovieBindings()
        {
            this.layoutControl1.Enabled = !MovieDBFactory.IsMultiSelected;

            if (MovieDBFactory.IsMultiSelected)
            {
                return;
            }

            xmlPreviewMediaInfoOutput.Clear();
            cmbFiles.Properties.Items.Clear();
            foreach (var file in MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media)
            {
                cmbFiles.Properties.Items.Add(file.PathAndFilename);
            }

            cmbFiles.SelectedIndex = 0;
            PopulateMediaInfoModel(MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].MiResponseModel);
            PopulateFileInfo();
        }

        private void RefreshTvBindings()
        {
            xmlPreviewMediaInfoOutput.Clear();
            cmbFiles.Properties.Items.Clear();
            cmbFiles.Properties.Items.Add(TvDBFactory.CurrentEpisode.FilePath.PathAndFilename);
            cmbFiles.SelectedIndex = 0;
            this.PopulateMediaInfoModel(TvDBFactory.CurrentEpisode.FilePath.MiResponseModel);
            this.PopulateFileInfo();
        }

        private void PopulateMediaInfoModel(MiResponseModel miResponseModel)
        {
            if (miResponseModel == null)
            {
                return;
            }
        }

        /// <summary>
        /// Handles the FocusedRowChanged event of the grdViewSubtitleStreams control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs"/> instance containing the event data.</param>
        private void grdViewSubtitleStreams_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnSubtitleUp.Enabled = grdViewSubtitleStreams.GetSelectedRows().Length > 0;
            btnSubtitleDown.Enabled = grdViewSubtitleStreams.GetSelectedRows().Length > 0;
            btnSubtitleRemove.Enabled = grdViewSubtitleStreams.GetSelectedRows().Length > 0;
        }

        /// <summary>
        /// Handles the FocusedRowChanged event of the grdViewAudioStreams control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs"/> instance containing the event data.</param>
        private void grdViewAudioStreams_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnAudioUp.Enabled = grdViewAudioStreams.GetSelectedRows().Length > 0;
            btnAudioDown.Enabled = grdViewAudioStreams.GetSelectedRows().Length > 0;
            btnAudioRemove.Enabled = grdViewAudioStreams.GetSelectedRows().Length > 0;
        }

    }
}
