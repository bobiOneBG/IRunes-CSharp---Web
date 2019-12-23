namespace SIS.MvcFramework
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Responses;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Attributes.Action;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Result;
    using SIS.WebServer.Routing;
    using SIS.WebServer.Routing.Contracts;
    using SIS.WebServer.Sessions;
    using System;
    using System.Linq;
    using System.Reflection;

    public static class WebHost
    {
        private const BindingFlags BindingFlagsConstant =
              BindingFlags.DeclaredOnly |
                  BindingFlags.Public |
                  BindingFlags.Instance;

        public static void Start(IMvcApplication application)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            IHttpSessionStorage sessionStorage = new HttpSessionStorage();

            AutoRegisterRoutes(application, serverRoutingTable);

            application.ConfigureServices();

            application.Configure(serverRoutingTable);

            Server server = new Server(8000, serverRoutingTable, sessionStorage);
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
                    .Where(m => !m.IsSpecialName && m.DeclaringType == controller)
                    .Where(m => m.GetCustomAttributes()
                    .All(a => a.GetType() != typeof(NonActionAttribute)));

                foreach (var action in actions)
                {
                    var attribute = (BaseHttpAttribute)action.GetCustomAttributes()
                        .Where(a => a.GetType()
                        .IsSubclassOf(typeof(BaseHttpAttribute)))
                        .LastOrDefault();

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
                        ((Controller)controllerInstance).Request = request;

                        // Security Authorization - TODO: Refactor this
                        var controllerPrincipal = ((Controller)controllerInstance).User;
                        var authorizeAttribute = action.GetCustomAttributes()
                            .LastOrDefault(a => a.GetType() == typeof(AuthorizeAttribute)) as AuthorizeAttribute;

                        if (authorizeAttribute != null && !authorizeAttribute.IsInAuthority(controllerPrincipal))
                        {
                            // TODO: Redirect to configured URL
                            return new HttpResponse(HttpResponseStatusCode.Forbidden);
                        }

                        var response = (ActionResult)action
                            .Invoke(controllerInstance, new object[0]);

                        return response;
                    });
                }
            }
        }
    }
}