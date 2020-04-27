using Swashbuckle.Application;
using System.Web.Http;
using WebApiNamespaceRouting.Routing;

namespace Sample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration httpConfiguration)
        {
            // Web API configuration and services
            httpConfiguration.EnableNamespaceRouting();

            httpConfiguration.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "WebApi Namespace Routing Service Documentation");
                c.UseFullTypeNameInSchemaIds();
                c.GroupActionsBy(apiDesc =>
                {
                    return apiDesc.RelativePath.Substring(0, apiDesc.RelativePath.IndexOf("/")) + "-" +
                                apiDesc.ActionDescriptor.ControllerDescriptor.ControllerName;
                }
                );
            })
            .EnableSwaggerUi();

            // Web API routes
            httpConfiguration.MapHttpAttributeRoutes();

            // Routing for all controllers in 'Sample.Controllers.ClientApi' and nested namespaces (namesapce ends with *)
            var clientRoute = httpConfiguration.Routes.MapHttpRoute(
                name: "ClientApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: null,
                dataTokens: new { Namespaces = new string[] { "Sample.Controllers.ClientApi*" } }
            );

            // Routing for all controllers in 'Sample.Controllers.DashboardApi' and nested namespaces (namesapce ends with *)
            var dashboardRoute = httpConfiguration.Routes.MapHttpRoute(
                name: "DashboardApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: null,
                dataTokens: new { Namespaces = new string[] { "Sample.Controllers.DashboardApi*" } }
            );

            // Routing for all controllers in 'Sample.Controllers.CommonApi' namespace. It doesn't include nested namespaces (namesapce ends with .)
            var onlyCommonRoute = httpConfiguration.Routes.MapHttpRoute(
                name: "OnlyCommonApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: null,
                dataTokens: new { Namespaces = new string[] { "Sample.Controllers.CommonApi." } }
            );

            // Routing for all controllers in 'Sample.Controllers.CommonApi' and nested namespaces (namesapce ends with *)
            var allCommonRoute = httpConfiguration.Routes.MapHttpRoute(
                name: "AllCommonApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: null,
                dataTokens: new { Namespaces = new string[] { "Sample.Controllers.CommonApi*" } }
            );

            // Routing for all controllers in 'Sample.Controllers.OtherApi' and 'Sample.Controllers.CommonApi' and nested namespaces (namesapce ends with *)
            var otherRoute = httpConfiguration.Routes.MapHttpRoute(
                name: "OtherApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: null,
                dataTokens: new { Namespaces = new string[] { "Sample.Controllers.OtherApi*", "Sample.Controllers.CommonApi*" } }
            );
        }
    }
}

