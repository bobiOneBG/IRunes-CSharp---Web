﻿namespace SIS.Mvc.Framework.DependencyConrainer
{
    using System;

    public interface IServiceProvider
    {
        void Add<TSource, TDestination>()
            where TDestination : TSource;

        object CreateInstance(Type type);

        //T CreateInstance<T>();
    }
}