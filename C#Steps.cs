// Startup.cs add
using Microsoft.Extensions.Logging;

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory LoggerFactory)
{
            LoggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
                
     app.Run(async (context) =>
     {
         await context.Response.WriteAsync("Hello World!");
     });
}

// Command line
set ASPNETCORE_ENVIRONMENT=Development




// .csProj
<ItemGroup>
  <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="2.0.0" />
</ItemGroup>
// run dotnet restore
// dotnet watch run to test

// Command line run
dotnet add package Microsoft.AspNetCore.Mvc -v=2.0.2

// Replace startup .cs with
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
}
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    loggerFactory.AddConsole();
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    app.UseMvc();
}

// Create Controllers directory

// Insert this into controller
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace YourNamespace.Controllers
{
    public class HelloController : Controller
    {
        [HttpGetAttribute]
        public string Index()
        {
            return "Hello World!";
        }
    }
}

// Insert new route into Startup.cs
// Other code
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    //Debug code...
    app.UseMvc( routes =>
    {
        routes.MapRoute(
            name: "Default", // The route's name is only for our own reference
            template: "", // The pattern that the route matches
            defaults: new {controller = "Hello", action = "Index"} // The controller and method to execute
        );
    });
}


// Update Controller
[HttpGet]
[Route("")]
public string Index()
{
    return "Hello World!";
}
 
     [HttpPost]
        [Route("post")]
        public IActionResult Other()
        {
            // Return a view (We'll learn how soon!)
            return null;
        }

// Also update Startup.cs
public void Configure(IApplicationBuilder app)
{
    app.UseMvc(); 
}

//  THE FINAL PRODUCT SHOULD LOOK LIKE THIS //

//*************** */ .csProj **************//

<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <Folder Include="wwwroot\" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.5" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.0.2" />
  </ItemGroup>

<ItemGroup>
  <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="2.0.0" />
</ItemGroup>
</Project>
//*************** */ END .csProj **************//

//*************** */ Startup.cs **************//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CallingCard
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }

    }
}

//*************** */ END Startup.cs **************//

//*************** */ HomeController.cs **************//
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class HelloController : Controller
    {
        [HttpGet]
        [Route("index")]
        public string Index()
        {
            return "Goodbye World!";
        }

        // A POST method
        [HttpPost]
        [Route("post")]
        public IActionResult Other()
        {
            // Return a view (We'll learn how soon!)
            return null;
        }
    }
}
//*************** */ END HomeController.cs **************//
