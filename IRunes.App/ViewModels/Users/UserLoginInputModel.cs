namespace IRunes.App.ViewModels.Users
{
    using SIS.Mvc.Framework.Attributes.Validation;

    public class UserLoginInputModel
    {
        private const string ErrorMessage = "Invalid username or password!";

        [RequiredSIS(ErrorMessage)]
        public string Username { get; set; }

        [RequiredSIS(ErrorMessage)]
        public string Password { get; set; }
    }
}