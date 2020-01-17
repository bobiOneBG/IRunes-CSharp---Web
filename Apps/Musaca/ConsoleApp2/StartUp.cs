namespace Musaca.Web
{
    using Musaca.Data;
    using SIS.MvcFramework;
    using SIS.WebServer.Routing.Contracts;
    using IServiceProvider = SIS.Mvc.Framework.DependencyConrainer.IServiceProvider;

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

        }
    }
}