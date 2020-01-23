namespace Musaca.Web.Controllers
{
    using Musaca.Services;
    using Musaca.Web.ViewModels.Products;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Result;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [Authorize]
        public IActionResult All()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }


        [HttpPost]
        [Authorize]        
        public IActionResult Create(CreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Products/Create");
            }

            this.productsService.Create(model.Name, model.Price);

            return Redirect("/Products/All");
        }
    }
}