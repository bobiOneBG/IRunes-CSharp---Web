namespace SIS.Mvc.Framework.DependencyConrainer
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ServiceProvider : SIS.Mvc.Framework.DependencyConrainer.IServiceProvider
    {
        private readonly IDictionary<Type, Type> dependencyContainer =
            new ConcurrentDictionary<Type, Type>();

        public IDictionary<Type, Type> DependencyContainer => this.dependencyContainer;

        public void Add<TSource, TDestination>()
            where TDestination : TSource
        {
            DependencyContainer[typeof(TSource)] = typeof(TDestination);
        }
         
        public object CreateInstance(Type type)
        {
            Console.WriteLine(type.Name);
            if (DependencyContainer.ContainsKey(type))
            {
                type = DependencyContainer[type];
            }

            var constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                 .OrderBy(x => x.GetParameters().Count()).FirstOrDefault();

            if (constructor == null)
            {
                return null;
            }

            var parameters = constructor.GetParameters();
            var parametersInstances = new List<object>();
            foreach (var parameter in parameters)
            {
                var parameterInstance = CreateInstance(parameter.ParameterType);
                parametersInstances.Add(parameterInstance);
            }

            var obj = constructor.Invoke(parametersInstances.ToArray());
            return obj;
        }
    }
}