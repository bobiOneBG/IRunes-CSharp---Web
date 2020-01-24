namespace Musaca.Web.Controllers
{
    using Musaca.Services;
    using Musaca.Web.ViewModels.Products;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Result;
    using System.Linq;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult All()
        {
            var allProducts = productsService.GetAllProducts()
                .Select(x => new AllProductsViewModel
                {
                    Name=x.Name,
                    Price=x.Price
                });

            return this.View(allProducts);
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