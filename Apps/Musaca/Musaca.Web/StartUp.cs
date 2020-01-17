namespace Musaca.Web
{
    using Musaca.Data;
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
        }
    }
}