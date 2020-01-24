namespace Musaca.Web.Controllers
{
    using Musaca.Services;
    using Musaca.Web.ViewModels.Orders;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Result;
    using System.Linq;

    public class OrdersController : Controller
    {
        private readonly OrdersService ordersService;
        private readonly IUsersService usersService;

        public OrdersController(OrdersService ordersService, IUsersService usersService)
        {
            this.ordersService = ordersService;
            this.usersService = usersService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Order(OrdersAddProductModel model)
        {
            this.ordersService.AddProductToOrder(this.User.Id, model.Product);

            var products = this.ordersService.GetCurrentOrderProducts(this.User.Id)
                .Select(x => new OrderProductsViewModel
                {
                    Name = x.Name,
                    Price = x.Price
                });

            return this.View(new OrderListProductsViewModel { Products = products });
        }

        [Authorize]
        public IActionResult Cashout()
        {
            ordersService.CashoutOrder(this.User.Id);

            usersService.CreateOrderIfIsNotActive(this.User.Id);

            return this.Redirect("/");
        }        
    }
}