using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using APaers.DataGen.AppBase;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Owin;

namespace APaers.DataGen.WebApi
{
    public partial class Startup
    {
        public void ConfigureContainer(IAppBuilder app)
        {
            ContainerBuilder builder = new ContainerBuilder();
            DependencyRegistrator.RegisterModules(builder);

            Assembly assembly = typeof(WebApiApplication).Assembly;
            // WebApi
            HttpConfiguration config = GlobalConfiguration.Configuration;
            // This for OWIN integration(OWIN hosting): HttpConfiguration config = new HttpConfiguration();
            builder.RegisterApiControllers(assembly);
            // OPTIONAL: Register the Autofac filter provider: builder.RegisterWebApiFilterProvider(config);

            // MVC
            builder.RegisterControllers(assembly);

            IContainer container = builder.Build();

            // MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            // WebApi
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseAutofacMvc();
        }
    }
}