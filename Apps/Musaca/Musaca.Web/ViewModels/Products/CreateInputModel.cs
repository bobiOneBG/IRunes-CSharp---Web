namespace Musaca.Web.ViewModels.Products
{
    using SIS.Mvc.Framework.Attributes.Validation;
    using System.ComponentModel.DataAnnotations;
    public class CreateInputModel
    {
        private const string PriceErrorMessage = "Price shold be Larger than 0.01";
        private const string NameErrorMessage = "Product name length should be between 3 and 10 symbols";

        [Required]
        [StringLenghtSIS(3, 10, NameErrorMessage)]
        public string Name { get; set; }

        [RangeSIS(typeof(decimal), "0.01", "14264337593543950335", PriceErrorMessage)]
        public decimal Price { get; set; }
    }
}
