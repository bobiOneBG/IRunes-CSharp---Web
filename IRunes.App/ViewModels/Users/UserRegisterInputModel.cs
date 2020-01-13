namespace IRunes.App.ViewModels.Users
{
    using SandBox;
    using SIS.Mvc.Framework.Attributes.Validation;

    public class UserRegisterInputModel
    {
        private const string UsernameErrorMessage =
            @"Invalid username length! Must be between 5 and 20 symbols!";

        private const string PasswordErrorMessage =
            "Invalid password length! Password must be at least 6 symbols";

        [RequiredSIS]
        [StringLenghtSIS(5, 20, UsernameErrorMessage)]
        public string Username { get; set; }

        [RequiredSIS]
        [StringLenghtSISAttribute(6,255, PasswordErrorMessage)]
        [PassswordSIS(nameof(ConfirmPassword))]
        public string Password { get; set; }

        [RequiredSIS]
        public string ConfirmPassword { get; set; }

        [RequiredSIS]
        [EmailSIS]
        public string Email { get; set; }
    }
}