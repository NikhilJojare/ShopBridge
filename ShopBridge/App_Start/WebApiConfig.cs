using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Query;

namespace ShopBridge
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
          
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<ItemMaster>("ItemMasters");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            config.AddODataQueryFilter(new EnableQueryAttribute
            {
                AllowedQueryOptions = AllowedQueryOptions.All

            });

            //config.
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);


            //config.Filters.Add(new BasicAuthenticationAttribute());
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
        }
    }
}
