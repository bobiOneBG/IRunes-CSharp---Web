namespace IRunes.App.ViewModels.Tracks
{
    using SIS.Mvc.Framework.Attributes.Validation;

    public class TrackDetailsInputModel
    {
        [RequiredSIS]
        public string AlbumId { get; set; }

        [RequiredSIS]
        public string TrackId { get; set; }
    }
}
