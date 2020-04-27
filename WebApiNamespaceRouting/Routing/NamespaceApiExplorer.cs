using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;

namespace WebApiNamespaceRouting.Routing
{
    public class NamespaceApiExplorer : IApiExplorer
    {
        private IApiExplorer _innerApiExplorer;
        private HttpConfiguration _configuration;
        private Lazy<Collection<ApiDescription>> _apiDescriptions;

        public NamespaceApiExplorer(IApiExplorer apiExplorer, HttpConfiguration configuration)
        {
            _innerApiExplorer = apiExplorer;
            _configuration = configuration;
            _apiDescriptions = new Lazy<Collection<ApiDescription>>(
                new Func<Collection<ApiDescription>>(Init));
        }

        public Collection<ApiDescription> ApiDescriptions
        {
            get { return _apiDescriptions.Value; }
        }

        private Collection<ApiDescription> Init()
        {
            var descriptions = _innerApiExplorer.ApiDescriptions;

            var result = new Collection<ApiDescription>();

            foreach (var description in descriptions)
            {
                var namespaces = description.Route.DataTokens["Namespaces"] as IEnumerable<string>;
                var controllerDesc = description.ActionDescriptor.ControllerDescriptor.ControllerType;
                bool add = false;
                if (namespaces != null && namespaces.Count() > 0)
                {
                    foreach (var ns in namespaces)
                    {
                        if (ns.EndsWith("*"))
                        {
                            var n = ns.Substring(0, ns.Length - 1);
                            if (controllerDesc.FullName.StartsWith(n))
                            {
                                add = true;
                                break;
                            }
                        }
                        else
                        {
                            if (controllerDesc.FullName.Equals(ns + controllerDesc.Name))
                            {
                                add = true;
                                break;
                            }
                        }
                    }
                    if (add == true)
                    {
                        var ctrlName = description.ActionDescriptor.ControllerDescriptor.ControllerName;
                        var duplicateName = string.Format("{0}-{1}", ctrlName, controllerDesc.Namespace);
                        description.RelativePath = description.RelativePath.Replace(duplicateName, ctrlName);
                    }
                }
                else
                    add = true;

                if (add)
                    result.Add(description);
            }
            return result;
        }

        public static IEnumerable<IHttpRoute> FlattenRoutes(IEnumerable<IHttpRoute> routes)
        {
            var flatRoutes = new List<HttpRoute>();

            foreach (var route in routes)
            {
                if (route is HttpRoute)
                    yield return route;

                var subRoutes = route as IReadOnlyCollection<IHttpRoute>;
                if (subRoutes != null)
                    foreach (IHttpRoute subRoute in FlattenRoutes(subRoutes))
                        yield return subRoute;
            }
        }
    }
}