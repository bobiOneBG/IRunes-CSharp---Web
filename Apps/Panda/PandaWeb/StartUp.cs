namespace PandaWeb
{
    using Panda.Data;
    using Panda.Services;
    using SIS.MvcFramework;
    using SIS.WebServer.Routing.Contracts;
    using IServiceProvider = SIS.Mvc.Framework.DependancyConrainer.IServiceProvider;

    public class StartUp : IMvcApplication
    {
        public void Configure(IServerRoutingTable serverRoutingTable)
        {
            using (var db = new PandaDbContext())
            {
                db.Database.EnsureCreated();
            }
        }

        public void ConfigureServices(IServiceProvider serviceProvider)
        {
            serviceProvider.Add<IUsersSevice, UsersSevice>();
        }
    }
}