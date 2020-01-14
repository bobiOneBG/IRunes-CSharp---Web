namespace PandaWeb.VIewModel.Users
{
    using SIS.Mvc.Framework.Attributes.Validation;

    public class LoginInputModel
    {
        [RequiredSIS]
        [StringLenghtSIS(5, 20, "Username should be between 5 and 20 symbols")]
        public string Username { get; set; }

        [RequiredSIS]
        public string Password { get; set; }
    }
}