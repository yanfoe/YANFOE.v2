using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YANFOE.UI.Popups
{
    public class EpisodeTreeList
    {
        public int id { set; get; }
        public int parent { set; get; }
        public string seriesname { set; get; }
        public int missingEpisodesCount { set; get; }
        public string episodename { set; get; }
    }
}
