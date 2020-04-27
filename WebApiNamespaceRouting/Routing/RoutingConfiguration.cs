using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;

namespace WebApiNamespaceRouting.Routing
{
    public static class NamespaceRoutingConfiguration
    {
        public static void EnableNamespaceRouting(this HttpConfiguration config)
        {
            config.Services.Replace(typeof(IHttpControllerSelector),
                new NamespaceHttpControllerSelector(GlobalConfiguration.Configuration));
            config.Services.Replace(typeof(IApiExplorer),
                new NamespaceApiExplorer(GlobalConfiguration.Configuration.Services.GetApiExplorer(), GlobalConfiguration.Configuration));
        }
    }
}
