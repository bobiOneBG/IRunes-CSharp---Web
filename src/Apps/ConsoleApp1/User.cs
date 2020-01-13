namespace SandBox
{
    public class User
    {
        [PassswordSIS]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}