namespace Musaca.Web.Controllers
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Result;

    public class HomeController : Controller
    {
        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        private IActionResult Index()
        {
            if (this.IsLoggedIn())
            {
                return this.View("IndexLoggedin");
            }

            return this.View();
        }
    }
}