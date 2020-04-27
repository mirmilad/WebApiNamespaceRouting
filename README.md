## WebApi Namespace Routing
A simple library to write http routes based on controllers namespace. This library allows you having same controller name in different namespaces and customize routing of your controllers based on namespace.

## Usage
### Enabling Namespace Routing
Add `WebApiNamespaceRouting` project or compiled binary DLL to your project as a reference. Then enable Namespace routing by invoking the following extension method (in namespace WebApiNamespaceRouting.Routing) in WebApiConfig.cs (for IIS hosted) or Startup.cs (for OWIN) or Program.cs (for Self hosted) file:
```csharp
httpConfiguration.EnableNamespaceRouting();
```
### Adding Routes
Add your routes by calling `MapHttpRoute` extension method (in namespace WebApiNamespaceRouting.Routing). 
```csharp
var clientRoute = httpConfiguration.Routes.MapHttpRoute(
	name: "ClientApi",
	routeTemplate: "{controller}/{action}/{id}",
	defaults: new { id = RouteParameter.Optional },
	constraints: null,
	handler: null,
	dataTokens: new { Namespaces = new string[] { "Sample.Controllers.ClientApi*" } }
);
```
It's similar to the standard WebApi routing, except that you must provide desired namespaces template. This route includes controllers in provided namespaces. If a namespace template ends with `.` character, it means that route includes all controllers inside that namespace but if namespace template ends with `*` charachter it means this route includes all controllers in that namespace and inner namespaces. 
The final route template is a combination of route name and route template it is `{routeName}/{routeTemplate}`. For exmple, the final route template of above code is `ClientApi/{controller}/{action}/{id}`.
### Same controller name
This library allows you having controllers with same name in different namespaces. In this case, you must have different route for each namespace to seperate controllers. Suppose you have two controllers named UserController in Sample.Controllers.ClientApi and Sample.Controllers.DashboardApi namespaces. Then your routing should be like this:
```csharp
// Routing for all controllers in 'Sample.Controllers.ClientApi' and nested namespaces // Routing for all controllers in 'Sample.Controllers.ClientApi' and nested namespaces (namesapce ends with *)
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
```
### Multiple namespaces in one route
You can have a single route for multiple namespaces. 
```csharp
// Routing for all controllers in 'Sample.Controllers.OtherApi' and 'Sample.Controllers.CommonApi' and nested namespaces (namesapce ends with *)
var otherRoute = httpConfiguration.Routes.MapHttpRoute(
	name: "OtherApi",
	routeTemplate: "{controller}/{action}/{id}",
	defaults: new { id = RouteParameter.Optional },
	constraints: null,
	handler: null,
	dataTokens: new { Namespaces = new string[] { "Sample.Controllers.OtherApi*", "Sample.Controllers.CommonApi*" } }
);
```
This route includes all controllers in Sample.Controllers.OtherApi and Sample.Controllers.CommonApi namespaces.
### Swagger configuration
If you have decided to use Swagger, you must add custom grouping policy. Use the following code snippet to configure swagger correctly:
```csharp
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
```
This configuration generates `{routeName}-{controllerName}` as grouping key in swagger UI.
## More Info
For more information and samples please have a look at Sample project.