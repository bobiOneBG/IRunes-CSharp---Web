namespace IRunes.App
{
    using IRunes.Data;
    using SIS.MvcFramework;
    using SIS.WebServer.Routing.Contracts;

    public class StartUp : IMvcApplication
    {
        public void Configure(IServerRoutingTable serverRoutingTable)
        {
            using (var context = new RunesDbContext())
            {
                context.Database.EnsureCreated();
            }
        }

        public void ConfigureServices()
        {
        }
    }
}