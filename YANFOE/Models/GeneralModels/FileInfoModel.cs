namespace YANFOE.Models.GeneralModels
{
    using System.ComponentModel;

    public class FileInfoModel
    {
        public FileInfoModel()
        {
            this.AudioStreams = new BindingList<AudioStreamModel>();
            this.SubtitleStreams = new BindingList<SubtitleStreamModel>();
        }

        public string Codec { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public string AspectRatioDecimal { get; set; }

        public string AspectRatioPercentage { get; set; }

        public string Resolution { get; set; }

        public string FPS { get; set; }

        public bool Progressive { get; set; }

        public bool Interlaced { get; set; }

        public bool PAL { get; set; }

        public bool NTSC { get; set; }

        public BindingList<AudioStreamModel> AudioStreams { get; set; }

        public BindingList<SubtitleStreamModel> SubtitleStreams { get; set; }

    }
}
