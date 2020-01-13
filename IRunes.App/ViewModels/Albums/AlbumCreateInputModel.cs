namespace IRunes.App.ViewModels.Albums
{
    using SIS.Mvc.Framework.Attributes.Validation;

    public class AlbumCreateInputModel
    {
        private const string NameErrorMessage = "Invalid length! Name must be between 3 and 30 symbols!";

        private const string CoverErrorMessage = "Invalid length! Cover must be between 5 and 255 symbols!";

        [RequiredSIS]
        [StringLenghtSIS(3, 30, NameErrorMessage)]
        public string Name { get; set; }

        [RequiredSIS]
        [StringLenghtSIS(5, 255, CoverErrorMessage)]
        public string Cover { get; set; }
    }
}