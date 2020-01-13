namespace SIS.MvcFramework
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses;
    using SIS.HTTP.Responses.Contracts;
    using SIS.Mvc.Framework;
    using SIS.Mvc.Framework.Attributes.Validation;
    using SIS.Mvc.Framework.DependencyConrainer;
    using SIS.Mvc.Framework.Logging;
    using SIS.Mvc.Framework.Validation;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Attributes.Action;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Result;
    using SIS.WebServer.Routing;
    using SIS.WebServer.Routing.Contracts;
    using SIS.WebServer.Sessions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class WebHost
    {
        private static readonly IControllerState controllerState = new ControllerState();

        public static void Start(IMvcApplication application)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            IHttpSessionStorage httpSessionStorage = new HttpSessionStorage();
            Mvc.Framework.DependancyConrainer.IServiceProvider serviceProvider = new ServiceProvider();
            serviceProvider.Add<ILogger, ConsoleLogger>();

            application.ConfigureServices(serviceProvider);

            AutoRegisterRoutes(application, serverRoutingTable, serviceProvider);
            application.Configure(serverRoutingTable);
            var server = new Server(8000, serverRoutingTable, httpSessionStorage);
            server.Run();
        }

        private static void AutoRegisterRoutes(
            IMvcApplication application,
            IServerRoutingTable serverRoutingTable,
            Mvc.Framework.DependancyConrainer.IServiceProvider serviceProvider)
        {
            var controllers = application.GetType().Assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract
                    && typeof(Controller).IsAssignableFrom(type));
            foreach (var controllerType in controllers)
            {
                var actions = controllerType
                    .GetMethods(BindingFlags.DeclaredOnly
                    | BindingFlags.Public
                    | BindingFlags.Instance)
                    .Where(x => !x.IsSpecialName && x.DeclaringType == controllerType)
                    .Where(x => x.GetCustomAttributes().All(a => a.GetType() != typeof(NonActionAttribute)));

                foreach (var action in actions)
                {
                    var path = $"/{controllerType.Name.Replace("Controller", string.Empty)}/{action.Name}";
                    var attribute = action.GetCustomAttributes().Where(
                        x => x.GetType().IsSubclassOf(typeof(BaseHttpAttribute))).LastOrDefault() as BaseHttpAttribute;
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
                        path = $"/{controllerType.Name.Replace("Controller", string.Empty)}/{attribute.ActionName}";
                    }

                    serverRoutingTable.Add(httpMethod, path,
                        (request) => ProcessRequest(serviceProvider, controllerType, action, request));

                    Console.WriteLine(httpMethod + " " + path);
                }
            }
        }

        private static IHttpResponse ProcessRequest(
            Mvc.Framework.DependancyConrainer.IServiceProvider serviceProvider,
            System.Type controllerType,
            MethodInfo action,
            IHttpRequest request)
        {
            var controllerInstance = serviceProvider.CreateInstance(controllerType) as Controller;
            controllerState.SetState(controllerInstance);

            controllerInstance.Request = request;

            // Security Authorization - TODO: Refactor this
            var controllerPrincipal = controllerInstance.User;

            if (action.GetCustomAttributes()
                .LastOrDefault(a => a.GetType() == typeof(AuthorizeAttribute)) 
                is AuthorizeAttribute authorizeAttribute && 
                !authorizeAttribute.IsInAuthority(controllerPrincipal))
            {
                // TODO: Redirect to configured URL
                return new HttpResponse(HttpResponseStatusCode.Forbidden);
            }

            var parameters = action.GetParameters();
            var parameterValues = new List<object>();

            foreach (var parameter in parameters)
            {
                ISet<string> httpDataValue = TryGetHttpParameter(request, parameter.Name);

                if (parameter.ParameterType
                    .GetInterfaces()
                    .Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IEnumerable<>) &&
                    parameter.ParameterType != typeof(string)))
                {
                    var collection = httpDataValue.Select(x => System.Convert.ChangeType(
                          x, parameter.ParameterType.GenericTypeArguments.First()));

                    parameterValues.Add(collection);
                    continue;
                }

                try
                {
                    string httpStringValue = httpDataValue.FirstOrDefault();

                    var parameterValue = System.Convert
                        .ChangeType(httpStringValue, parameter.ParameterType);
                    parameterValues.Add(parameterValue);
                }
                catch
                {
                    var parameterValue = System.Activator.CreateInstance(parameter.ParameterType);

                    var properties = parameter.ParameterType.GetProperties();

                    foreach (var property in properties)
                    {
                        ISet<string> propertyHttpDataValue = TryGetHttpParameter(request, property.Name);

                        var firstValue = propertyHttpDataValue.FirstOrDefault();

                        var propertyValue = System.Convert.ChangeType(firstValue, property.PropertyType);

                        property.SetMethod.Invoke(parameterValue, new object[] { propertyValue });
                    }

                    if (request.RequestMethod==HttpRequestMethod.Post)
                    {
                        controllerState.Reset();

                        controllerInstance.ModelState = ValidateObject(parameterValue);

                        controllerState.Initialize(controllerInstance);
                    }
                    
                    parameterValues.Add(parameterValue);
                }
            }

            var response = action.Invoke(controllerInstance, parameterValues.ToArray()) as ActionResult;
            return response;
        }

        private static ModelStateDictionary ValidateObject(object value)
        {
            var modelState = new ModelStateDictionary();

            var objectProperties = value.GetType().GetProperties();

            foreach (var objectProperty in objectProperties)
            {
                var validationAttributes = objectProperty
                    .GetCustomAttributes()
                    .Where(caType => caType is ValidationSISAttribute)
                    .Cast<ValidationSISAttribute>();

                foreach (var validationAttribute in validationAttributes)
                {
                    if (validationAttribute.IsValid(objectProperty.GetValue(value)))
                    {
                        continue;
                    }

                    modelState.Add(objectProperty.Name, validationAttribute.ErrorMessage);
                }
            }

            return modelState;
        }

        private static ISet<string> TryGetHttpParameter(IHttpRequest request, string parameterName)
        {
            parameterName = parameterName.ToLower();

            ISet<string> httpDataValue = null;  

            if (request.QueryData.Any(x => x.Key.ToLower() == parameterName))
            {
                httpDataValue = request.QueryData.FirstOrDefault(
                    x => x.Key.ToLower() == parameterName).Value;
            }
            else if (request.FormData.Any(x => x.Key.ToLower() == parameterName))
            {
                httpDataValue = request.FormData.FirstOrDefault(
                    x => x.Key.ToLower() == parameterName).Value;
            }

            return httpDataValue;
        }
    }
}