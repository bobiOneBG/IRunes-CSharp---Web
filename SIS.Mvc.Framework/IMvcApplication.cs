namespace SIS.MvcFramework
{
    using SIS.Mvc.Framework.DependencyConrainer;
    using SIS.WebServer.Routing.Contracts;

    public interface IMvcApplication
    {
        void Configure(IServerRoutingTable serverRoutingTable);

        void ConfigureServices(IServiceProvider serviceProvider);//DI
    }
}