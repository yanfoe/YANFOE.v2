// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Exporting.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.Exporting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using YANFOE.UI.Popups;
    using YANFOE.Factories;

    using DevExpress.XtraEditors;

    class Exporting
    {
        public static void ExportMissingTvShowEpisodes()
        {
            var missingEpisodes = new List<MissingEpisodeTreeList>();
            int i = 1;
            foreach (var series in TvDBFactory.TvDatabase)
            {
                if (series.Value.CountMissingEpisodes() > 0)
                {
                    var show = new MissingEpisodeTreeList();
                    show.id = i++;
                    show.seriesname = series.Value.SeriesName;
                    show.parent = 0;
                    missingEpisodes.Add(show);
                    var episodes = series.Value.GetMissingEpisodes();
                    foreach (var episode in episodes)
                    {
                        var ep = new MissingEpisodeTreeList();
                        ep.id = i++;
                        ep.parent = show.id;
                        ep.episodename = episode.EpisodeName;
                        ep.seriesname = string.Format("{0}x{1:00}", episode.GetSeason().SeasonNumber, episode.EpisodeNumber);
                        missingEpisodes.Add(ep);

                        show.missingEpisodesCount++;
                    }
                }
            }

            if (missingEpisodes.Count == 0)
            {
                XtraMessageBox.Show("No missing episodes found", "No result");
            }
            else
            {
                ExportMissingEpisodes form = new ExportMissingEpisodes(missingEpisodes);
                form.Show();
            }
        }

        public static bool ExportMissingEpisodesTemplate(string template)
        {
            return false;
        }
    }
}
