using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Web.Http;
using System.Web.Http.Cors;
using WebService.MessageHandlers;

[assembly: OwinStartup(typeof(WebService.Startup))]

namespace WebService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureRouting(config);

            ConfigureMessageHandlers(config);

            ConfigureMessageFormat(config);

            ConfigureCORS(config);

            app.UseWebApi(config);
        }

        /// <summary>
        /// Configure Routing
        /// </summary>
        /// <param name="config"></param>
        private void ConfigureRouting(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }

        private void ConfigureMessageHandlers(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new CheckCredentialsMessageHandler());
            config.MessageHandlers.Add(new CheckApiKeyMessageHandler());
        }

        /// <summary>
        /// Configure Json Media Type Formatting
        /// </summary>
        /// <param name="config"></param>
        private void ConfigureMessageFormat(HttpConfiguration config)
        {
            var formatter = config.Formatters.JsonFormatter;
            formatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            formatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects; // (OR: formatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; )
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }

        /// <summary>
        /// Configure Cross Origin Request
        /// </summary>
        /// <param name="config"></param>
        private void ConfigureCORS(HttpConfiguration config)
        {
            EnableCorsAttribute cors = new EnableCorsAttribute("http://messagehandlerexample.com", "*", "GET,POST,PUT,DELETE");
            config.EnableCors(cors);
        }
    }
}
