﻿namespace SIS.MvcFramework
{
    using SIS.WebServer;
    using SIS.WebServer.Routing;

    public static class WebHost
    {
        public static void Start(IMvcApplication application)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            application.ConfigureServices();

            application.Configure(serverRoutingTable);

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}