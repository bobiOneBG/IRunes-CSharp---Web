namespace IRunes.App.Controllers
{
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Result;
    using System.Collections.Generic;

    public class InfoController : Controller
    {
        public IHttpResponse About()
        {
            return this.View();
        }

        public ActionResult Json()
        {
            return Json(new List<object>
            {
                new
                {
                    Name = "Pesho",
                    Surname = "Peshev",
                    Email="Pesho@abv.bg",
                    Age="32"
                },new
                {
                    Name = "Pesho",
                    Surname = "Peshev",
                    Email = "Pesho@abv.bg",
                    Age = "32"
                }, new
                {
                    Name = "Pesho",
                    Surname = "Peshev",
                    Email = "Pesho@abv.bg",
                    Age = "32"
                }
            });
        }
    }
}