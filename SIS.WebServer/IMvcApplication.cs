namespace SIS.MvcFramework
{
    using SIS.WebServer.Routing;

    public interface IMvcApplication
    {
        void Configure(ServerRoutingTable serverRoutingTable);

        void ConfigureServices();//DI
    }
}