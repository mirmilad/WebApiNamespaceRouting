using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sample.App_Start
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                //c.PrettyPrint();
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
        }
    }
}