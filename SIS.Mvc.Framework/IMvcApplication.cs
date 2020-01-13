namespace SIS.MvcFramework
{
    using SIS.Mvc.Framework.DependancyConrainer;
    using SIS.WebServer.Routing.Contracts;

    public interface IMvcApplication
    {
        void Configure(IServerRoutingTable serverRoutingTable);

        void ConfigureServices(IServiceProvider serviceProvider);//DI
    }
}