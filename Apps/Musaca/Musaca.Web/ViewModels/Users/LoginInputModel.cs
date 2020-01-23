namespace Musaca.Web.ViewModels.Users
{
    using SIS.Mvc.Framework.Attributes.Validation;

    public class LoginInputModel
    {
        private const int minLength = 5;
        private const int maxLength = 20;
        private const string ErrorMessage = "Username length should be between 5 and 20 symbols";

        [RequiredSIS]
        [StringLenghtSIS(minLength, maxLength, ErrorMessage)]
        public string Username { get; set; }

        [RequiredSIS]
        public string Password { get; set; }
    }
}