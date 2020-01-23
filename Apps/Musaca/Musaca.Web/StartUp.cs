namespace Musaca.Web
{
    using Musaca.Data;
    using Musaca.Services;
    using SIS.Mvc.Framework.DependencyConrainer;
    using SIS.MvcFramework;
    using SIS.WebServer.Routing.Contracts;

    public class StartUp : IMvcApplication
    {
        public void Configure(IServerRoutingTable serverRoutingTable)
        {
            using (var db = new MusacaDbContext())
            {
                db.Database.EnsureCreated();
            }
        }

        public void ConfigureServices(IServiceProvider serviceProvider)
        {
            serviceProvider.Add<IUsersService, UsersService>();
            serviceProvider.Add<IProductsService, ProductsService>();
            serviceProvider.Add<IOrdersService, OrdersService>();
        }
    }
}