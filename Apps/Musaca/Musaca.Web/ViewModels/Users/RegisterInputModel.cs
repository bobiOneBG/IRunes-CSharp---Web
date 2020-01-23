using SIS.Mvc.Framework.Attributes.Validation;

namespace Musaca.Web.ViewModels.Users
{
    public class RegisterInputModel
    {
        private const string ErrorMessage = " length should be between 5 and 20 symbols";

        [RequiredSIS]
        [StringLenghtSIS(5, 20, "Username" + ErrorMessage)]
        public string Username { get; set; }

        [RequiredSIS]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        [RequiredSIS]
        [StringLenghtSIS(5, 20,"Email"+ ErrorMessage)]
        public string Email { get; set; }
    }
}