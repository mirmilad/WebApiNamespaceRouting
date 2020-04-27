using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WebApiNamespaceRouting.Routing
{
    public static class HttpRouteCollectionExtensions
    {
        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints, HttpMessageHandler handler, object dataTokens)
        {
            HttpRouteValueDictionary defaultsDictionary = new HttpRouteValueDictionary(defaults);
            HttpRouteValueDictionary constraintsDictionary = new HttpRouteValueDictionary(constraints);
            HttpRouteValueDictionary dataTokensDictionary = new HttpRouteValueDictionary(dataTokens);
            routeTemplate = name + "/" + routeTemplate;
            IHttpRoute route = routes.CreateRoute(routeTemplate, defaultsDictionary, constraintsDictionary, dataTokens: dataTokensDictionary, handler: handler);
            routes.Add(name, route);
            return route;
        }
    }
}