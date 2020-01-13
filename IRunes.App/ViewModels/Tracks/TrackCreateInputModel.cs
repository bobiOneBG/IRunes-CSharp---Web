namespace IRunes.App.ViewModels.Tracks
{
    using SIS.Mvc.Framework.Attributes.Validation;

    public class TrackCreateInputModel
    {
        private const string NameErrorMessage = "Track name must be between 3 and 20 symbols!";
        private const string LinkErrorMessage = "Link name must be longer than 3 symbols!";
        private const string PriceErrorMessage = "Invalid Price";

        [RequiredSIS]
        public string AlbumId { get; set; }

        [RequiredSIS]
        [StringLenghtSIS(3, 20, NameErrorMessage)]
        public string Name { get; set; }

        [RequiredSIS]
        [StringLenghtSIS(4, int.MaxValue, LinkErrorMessage)]
        public string Link { get; set; }

        [RangeSIS(typeof(decimal), "0.00", "79228162514264337593543950335", PriceErrorMessage)]
        public decimal Price { get; set; }
    }
}
