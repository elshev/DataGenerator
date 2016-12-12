using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using APaers.DataGen.Abstract.Repo;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using APaers.DataGen.Api.Infrastructure;
using APaers.DataGen.Api.Models.Auth;
using APaers.DataGen.Api.Providers;
using APaers.DataGen.AppBase;

[assembly: OwinStartup(typeof(APaers.DataGen.Api.Startup))]
namespace APaers.DataGen.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            MappingInitializer.Initialize();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            IContainer container = ConfigureContainer(app);
            ConfigureAuth(app);
            FilterConfig.RegisterGlobalFilters(config.Filters);
            WebApiConfig.Register(config);
            ILog log = container.Resolve<ILog>();
            config.Filters.Add(new DataGenExceptionFilterAttribute(log));
            config.Services.Replace(typeof(IExceptionHandler), new DataGenExceptionHandler(log));


            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.EnsureInitialized();
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationIdentityDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                Provider = new ApplicationOAuthProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public IContainer ConfigureContainer(IAppBuilder app)
        {
            ContainerBuilder builder = new ContainerBuilder();
            RegisterDependencies(builder);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            IContainer container = builder.Build();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            return container;
        }

        private void RegisterDependencies(ContainerBuilder builder)
        {
            DependencyRegistrator.RegisterModules(builder);
        }
    }
}