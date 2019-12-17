namespace SIS.MvcFramework
{
    using System;
    using System.Linq;
    using System.Reflection;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Responses.Contracts;
    using SIS.MvcFramework.Attributes;
    using SIS.WebServer;
    using SIS.WebServer.Routing;
    using SIS.WebServer.Routing.Contracts;

    public static class WebHost
    {
        private const BindingFlags BindingFlagsConstant =
              BindingFlags.DeclaredOnly |
                  BindingFlags.Public |
                  BindingFlags.Instance;
        public static void Start(IMvcApplication application)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            AutoRegisterRoutes(application, serverRoutingTable);

            application.ConfigureServices();

            application.Configure(serverRoutingTable);

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }

        private static void AutoRegisterRoutes(IMvcApplication application, IServerRoutingTable serverRoutingTable)
        {//TODO: Remove overrides
            var controllers = application.GetType().Assembly.GetTypes()
                 .Where(type => type.IsClass &&
                 !type.IsAbstract &&
                 typeof(Controller).IsAssignableFrom(type)
                 // &&type.IsSubclassOf(typeof(Controller))
                 );

            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods(BindingFlagsConstant)
                    .Where(m => !m.IsSpecialName);

                foreach (var action in actions)
                {
                    var attribute = (BaseHttpAttribute)action.GetCustomAttributes()
                        .Where(a => a.GetType().IsSubclassOf(typeof(BaseHttpAttribute))).LastOrDefault();

                    string path = $"/{controller.Name.Replace("Controller", string.Empty)}/{action.Name}";

                    var httpMethod = HttpRequestMethod.Get;

                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if (attribute?.Url != null)
                    {
                        path = attribute.Url;
                    }

                    if (attribute?.ActionName != null)
                    {
                        path = $"/{controller.Name.Replace("Controller", string.Empty)}/{attribute.ActionName}";
                    }

                    var url = httpMethod + " " + path;
                    Console.WriteLine(url);
                    serverRoutingTable.Add(httpMethod, path, request =>
                    {
                        var controllerInstance = Activator.CreateInstance(controller);
                        var response = (IHttpResponse)action
                            .Invoke(controllerInstance, new[] { request });

                        return response;
                    });
                }
            }
        }
    }
}