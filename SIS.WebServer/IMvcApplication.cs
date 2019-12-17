namespace SIS.MvcFramework
{
    using SIS.WebServer.Routing;
    using SIS.WebServer.Routing.Contracts;

    public interface IMvcApplication
    {
        void Configure(IServerRoutingTable serverRoutingTable);

        void ConfigureServices();//DI
    }
}