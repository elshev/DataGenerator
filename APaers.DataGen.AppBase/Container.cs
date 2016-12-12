using System;
using System.Collections.Generic;
using APaers.DataGen.Data.Json;
using APaers.DataGen.Data.MongoDb;
using Autofac;
using Autofac.Core;

namespace APaers.DataGen.AppBase
{
    public static class Container
    {
        private static readonly ContainerBuilder builder = new ContainerBuilder();
        public static IContainer InternalContainer { get; private set; }

        public static void Initialize(Action<ContainerBuilder> preConfigureAction, Action<ContainerBuilder> postConfigureAction)
        {
            preConfigureAction?.Invoke(builder);
            DependencyRegistrator.RegisterModules(builder);
            postConfigureAction?.Invoke(builder);
            InternalContainer = builder.Build();
        }

        public static void Initialize()
        {
            Initialize(null, null);
        }
    }

    public static class DependencyRegistrator
    {
        public static void RegisterModules(ContainerBuilder builder)
        {
            RegisterModules(builder, null);
        }

        public static void RegisterModules(ContainerBuilder builder, IEnumerable<IModule> modules)
        {
            //builder.RegisterModule(new MongoDbModule());
            builder.RegisterModule(new JsonModule());
            builder.RegisterModule(new DataGenModule());
            if (modules == null) return;
            foreach (IModule module in modules)
            {
                builder.RegisterModule(module);
            }
        }
    }
}