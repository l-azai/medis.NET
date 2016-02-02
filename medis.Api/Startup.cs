using medis.Api.Repositories;
using Newtonsoft.Json.Serialization;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System;

namespace medis.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) {
            //
            var config = new HttpConfiguration();

            ConfigureWebApi(config);

            ConfigureDependencyInjection(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);
        }

        public void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();            

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        public void ConfigureDependencyInjection(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            RegisterRepositories(container);
            RegisterManagers(container);

            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private void RegisterManagers(Container container)
        {
            var types = typeof(Repository<>).Assembly;

            var managers = types.GetExportedTypes()
                .Where(x => x.Namespace.StartsWith("medis.Api.Managers"))
                .Where(x => x.GetInterfaces().Any())
                .Select(type => new {
                    Service = type.GetInterfaces()
                        .Where(interfaces => interfaces
                            .Namespace
                            .StartsWith("medis.Api.Interfaces.Managers"))
                            .Single(),
                    Implementation = type
                });

            foreach (var mgr in managers)
            {
                container.Register(mgr.Service, mgr.Implementation, Lifestyle.Scoped);
            }
        }

        private void RegisterRepositories(Container container)
        {
            var types = typeof(Repository<>).Assembly;

            var repositories = types.GetExportedTypes()
                .Where(x => x.Namespace.StartsWith("medis.Api.Repositories."))
                .Where(x => x.GetInterfaces().Any())
                .Select(type => new {
                    Service = type.GetInterfaces()
                        .Where(interfaces => interfaces
                            .Namespace
                            .StartsWith("medis.Api.Interfaces.Repositories."))
                            .Single(),
                    Implementation = type
                });

            foreach (var rep in repositories)
            {
                container.Register(rep.Service, rep.Implementation, Lifestyle.Scoped);
            }
        }
    }
}