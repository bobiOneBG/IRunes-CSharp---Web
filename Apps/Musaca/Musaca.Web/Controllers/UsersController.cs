namespace Musaca.Web.Controllers
{
    using Musaca.Services;
    using Musaca.Web.ViewModels.Products;
    using Musaca.Web.ViewModels.Users;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Result;
    using System.Linq;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Users/Login");
            }

            var user = this.usersService.GetUserOrNull(model.Username, model.Password);

            if (user == null)
            {
                return this.Redirect("/Users/Login");
            }

             usersService.CreateOrderIfIsNotActive(user.Id);            

            this.SignIn(user.Id, user.Username, user.Email);

            return this.Redirect("/");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Users/Register");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Redirect("/Users/Register");
            }

            var userId = this.usersService.CreateUser(model.Username, model.Password, model.Email);

            this.usersService.CreateOrder(userId);

            this.SignIn(userId, model.Username, model.Email);

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Profile()
        {
            var orders = usersService.GetUserOrders(this.User.Id)
                .Select(x=>new AllOrdersViewModel 
                {
                    Id=x.Id,
                    IssuedOn=x.IssuedOn,
                    Price=x.Products.Sum(x=>x.Price),
                    Username=x.Cashier.Username
                });

            return this.View(orders);
        }

        [Authorize]
        public IActionResult Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
