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
    app.UseStaticFiles();
}


// *********************************** //
// *********************************** //
//  THE FINAL PRODUCT SHOULD LOOK LIKE THIS //
//  THE FINAL PRODUCT SHOULD LOOK LIKE THIS //
//  THE FINAL PRODUCT SHOULD LOOK LIKE THIS //
// *********************************** //
// *********************************** //

//*************** */ .csProj **************//
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <Folder Include="wwwroot\" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Session" Version="2.0.1" />
    <PackageReference Include="Microsoft.AspNetCore.Diagnostics" Version="2.0.0" />
    <PackageReference Include="Microsoft.Extensions.Logging.Console" Version="2.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Server.Kestrel" Version="2.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="2.0.1" />
    <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="1.1" />
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
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace CallingCard
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseMvc();
            app.UseStaticFiles();
        }

    }
}

//*************** */ END Startup.cs **************//

//*************** */ HomeController.cs **************//
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
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

<link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.3/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Zug+QiDoJOrZ5t4lssLdxGhVrurbmBWopoEl+M6BdEfwnCJZtKxi1KgxUyJq13dy" crossorigin="anonymous">

<link rel="stylesheet" href="~/css/style.css"/>



