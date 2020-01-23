namespace Musaca.Web.Controllers
{
    using Musaca.Services;
    using Musaca.Web.ViewModels.Orders;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Result;

    public class OrdersController : Controller
    {
        private readonly OrdersService ordersService;

        public OrdersController(OrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Order(OrdersAddProductModel model)
        {
            ordersService.AddProductToOrder(this.User.Id, model.Product);
            return Redirect("/Products/All");
        }

        //TO DO
        [Authorize]
        public IActionResult Cashout()
        {
            throw new System.Exception();
        }
    }
}