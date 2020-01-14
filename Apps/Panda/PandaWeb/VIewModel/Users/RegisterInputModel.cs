namespace PandaWeb.VIewModel.Users
{
    using SIS.Mvc.Framework.Attributes.Validation;

    public class RegisterInputModel
    {
        [RequiredSIS]
        [StringLenghtSIS(5,20, "Username should be between 5 and 20 symbols")]
        public string Username { get; set; }

        [RequiredSIS]
        [StringLenghtSIS(5, 20, "Email should be between 5 and 20 symbols")]
        public string Email { get; set; }

        [RequiredSIS]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}