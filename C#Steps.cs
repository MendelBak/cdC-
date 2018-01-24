STEPS FOR C SHARP!

dotnet new web // starts a new ASP.NET Core MVC app

// In Program.cs* //
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DbConnection;


namespace ajaxnotes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

// */end Program.cs //




// In Startup.cs* //
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace ajaxnotes
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSession();
            services.Configure<MySqlOptions>(Configuration.GetSection("DBInfo"));
            services.AddScoped<DbConnector>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();
        }

        public IConfiguration Configuration { get; private set; }
 
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

    }
}

// */end Startup.cs //



// Development Mode* // 
TO PUT WORK ENVIRONMENT IN DEVELOPMENT MODE, TYPE IN TERMINAL:

export ASPNETCORE_ENVIRONMENT=Development
;
// *end Development Mode // 




// appsettings.json* //
{
    "DBInfo":
    {
        "Name": "MySQLconnect",
        "ConnectionString": "server=localhost;userid=root;password=root;port=8889;database=mydb;SslMode=None"
    }
}
// *end appsettings.json //


// IFactory.cs* //
using lostinthewoods.Models;
using System.Collections.Generic;
namespace lostinthewoods.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
        
    }
}
// *end IFactory.cs //



// TrailFactory.cs* //
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using lostinthewoods.Models;


namespace lostinthewoods.Factory
{
    public class TrailFactory : IFactory<Trail>
    {
        private string connectionString;
        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(connectionString);
            }
        }
        public void Add(Trail item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO trails(name, description, elevationChange, length, lat, lon) VALUES (@name, @description, @elevationChange, @length, @lat, @lon)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public List<Trail> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails").ToList();
            }
        }
        public Trail FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }
    }
}
// *end TrailFactory.cs //




// DbConnection.cs* //
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;


namespace theWall
{
    public class DbConnector
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
 
        public DbConnector(IOptions<MySqlOptions> config)
        {
            MySqlConfig = config;
        }
        internal IDbConnection Connection {
            get {
                return new MySqlConnection(MySqlConfig.Value.ConnectionString);
            }
        }
        
        //This method runs a query and stores the response in a list of dictionary records
        public List<Dictionary<string, object>> Query(string queryString)
        {
            using(IDbConnection dbConnection = Connection)
            {
                using(IDbCommand command = dbConnection.CreateCommand())
                {
                   command.CommandText = queryString;
                   dbConnection.Open();
                   var result = new List<Dictionary<string, object>>();
                   using(IDataReader rdr = command.ExecuteReader())
                   {
                      while(rdr.Read())
                      {
                          var dict = new Dictionary<string, object>();
                          for( int i = 0; i < rdr.FieldCount; i++ ) {
                              dict.Add(rdr.GetName(i), rdr.GetValue(i));
                          }
                          result.Add(dict);
                      }
                   }
                   return result;
                }
            }
        }
        //This method run a query and returns no values
        public void Execute(string queryString)
        {
            using (IDbConnection dbConnection = Connection)
            {
                using(IDbCommand command = dbConnection.CreateCommand())
                {
                    command.CommandText = queryString;
                    dbConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
// */end DbConnection.cs //



// In .csproj* //
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <Folder Include="wwwroot\" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="Dapper" Version="1.50.4" />
    <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.3" />
    <PackageReference Include="Microsoft.AspNetCore.Identity" Version="2.0.1" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.0.1" />
    <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="1.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Session" Version="2.0.1" />
    <PackageReference Include="Microsoft.AspNetCore.Server.IISIntegration" Version="2.0.1" />
    <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="2.0.1" />
    <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="2.0.0" />
    <PackageReference Include="MySql.Data" Version="7.0.7-*" />
    <PackageReference Include="System.Data.SqlClient" Version="4.4.0-*" />
  </ItemGroup>
  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="2.0.0" />
  </ItemGroup>
</Project>
// *end .csproj //





// MVC* //
// Add dependencies required for ASP.NET Core MVC :

// dotnet add package Microsoft.AspNetCore.Mvc -v=1.1
// dotnet restore
// *end MVC //




// Controllers* //
1. Add a folder called Controllers, and name each file UniqueController.cs;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// using DbConnection;
using MySql.Data.MySqlClient;
using ajaxnotes.Models;

namespace ajaxnotes.Controllers
{
    public class NotesController : Controller
    {

        private readonly DbConnector _dbConnector;
 
        public NotesController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            string query2 = "SELECT * FROM notes";
            var allNotes = _dbConnector.Query(query2);
            ViewBag.allNotes = allNotes;
            return View("index");
        }

        [HttpPost]
        [Route("addnote")]
        public IActionResult addnote(string topic, string note)
        {
            string myTopic = '"'+ topic +'"';
            string myNote = '"'+ note +'"';
            string query = $"INSERT INTO notes (topic, note) VALUES ('{myTopic}', '{myNote}')";
            _dbConnector.Execute(query);
            return RedirectToAction("index");
        }


        [HttpGet]
        [Route("delete/{noteID}")]
        public IActionResult delete(int noteID)
        {
            string query = $"DELETE FROM notes WHERE id = {noteID}";
            _dbConnector.Execute(query);
            return RedirectToAction("index");
        }
 

        [HttpPost]
        [Route("add")]
        public IActionResult add(string first, string last, string age, string email, string pass)
        {
            var NewUser = new User
            {
                first_name = first,
                last_name = last,
                age = age,
                email = email,
                password = pass
            };

            if(TryValidateModel(NewUser) == false)
            {
                ViewBag.errors = ModelState.Values;
                return View("errors");
            }
            return RedirectToAction("success");
        }
    }
}





    // PARAMETERS //

        // [HttpGet]
        // [Route("template/{Name}")]
        // public IActionResult Method(string Name)
        // {
        //     // Method body
        // }


    // Returning JSON //

        // [HttpGet]
        // [Route("")]
        // public JsonResult Example()
        // {
        //     // The Json method convert the object passed to it into JSON
        //     return Json(SomeC#Object);
        // }

    // Other code
        // [HttpGet]
        // [Route("displayint")]
        // public JsonResult DisplayInt()
        // {
        //     return Json(34);
        // }
        
        // Suppose we're working with the Human class we wrote in the previous chapter
        // [HttpGet]
        // [Route("displayhuman")]
        // public JsonResult DisplayHuman()
        // {
        //     return Json(new Human());
        // }

    }
}
using ajaxnotes.Models;
public IActionResult ValidateUser()
        {
            User NewUser = new User
            {
                Name = "name",
                Email = "email@email.com",
                Password = "password"
            };
        
            TryValidateModel(NewUser); // Runs our validations
            // Other code
        }
// *end Controllers //


// index.cshtml* //
<!DOCTYPE html>
<html>
    <head>
        <meta charset='utf-8'>
        <title></title>
        <link rel="stylesheet" href="~/css/style.css"/>
    </head>
    <body>

        <h1>Welcome to QuotingDojo!</h1>
        <form action="/addquote" method="POST">
            Name: <input type="text" name="name"><br><br>
            Quote: <textarea name="quote" rows="2" cols="40"></textarea><br><br>
            <input type="submit" value="Add my quote!"><br>
        </form>
        <br>
            <a href="/quotes"><button>Skip to quotes!</button></a>

    </body>
</html>



        <table>
            <thead>
                <th>Name</th>
                <th>Quote</th>
                <th>Created at</th>
            </thead>
            @{
                @foreach(var i in @ViewBag.allQuotes)
                {
                int userID = i["id"];
                    <tr>
                        <td>@i["name"]</td>
                        <td>@i["quote"]</td>
                        <td>@i["created_at"]</td>
                        <td><a href="/delete/@userID"><button>Delete</button></a></td>
                    </tr>
                }
            }
        </table>
        <a href="/"><button>Back</button></a>

*** STANDARD() FORM() ***

    <form action="/add" method="POST">
        First Name: <input type="text" name="first"><br><br>
        Last Name: <input type="text" name="last"><br><br>
        Age: <input type="text" name="age"><br><br>
        Email: <input type="text" name="email"><br><br>
        Password: <input type="text" name="pass"><br><br>
        <input type="submit" value="Submit"><br>
    </form>

*** end() ***
// *end index.cshtml //

// MODEL User.cs* //
using System.ComponentModel.DataAnnotations;
 
namespace formSubmission.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        [Required]
        [MinLength(4)]
        public string first_name { get; set; }

        [Required]
        [MinLength(4)]
        public string last_name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public string age { get; set; }
 
        [Required]
        [EmailAddress]
        public string email { get; set; }
 
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
// *end MODEL User.cs //



// Properties/MySqlOptions.cs* //
namespace ajaxnotes
{
    public class MySqlOptions
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}
// *end MySqlOptions.cs //





// Ajax Refresh* //
$(document).ready(function(){
    // external request
    $.get("http://pokeapi.co/api/v2/pokemon/1", function(response){
        // Handle the response data
    })
    // back-end request
    $.get("/getusers", function(response){
        // Handle the response data
    })
})
// *end Ajax Refresh //




// Redirecting* //
RedirectToAction() if we want to redirect to other controller methods, rather than rendering a view.;

public class FirstController : Controller
{
    //  Other code
    public IActionResult Method()
    {
        // Will redirect to the "OtherMethod" method
        return RedirectToAction("OtherMethod");
    }
    // Other code
    public IActionResult OtherMethod()
    {
        return View();
    }
}
// *end Redirecting //


/
/
/
/
/
// ***** SESSION ***** //
using Microsoft.AspNetCore.Http in controller to use session;
HttpContext.Session.Clear(); to clear session!

// *Inside controller methods*
 
  
// To store a string in session we use ".SetString"
// The first string passed is the key and the second is the value we want to retrieve later
HttpContext.Session.SetString("UserName", "Samantha");
// To retrieve a string from session we use ".GetString"
string LocalVariable = HttpContext.Session.GetString("UserName");
 
// To store an int in session we use ".SetInt32"
HttpContext.Session.SetInt32("UserAge", 28);
 
// To retrieve an int from session we use ".GetInt32"
int? IntVariable = HttpContext.Session.GetInt32("UserAge");

using Newtonsoft.Json;
 
// Somewhere in your namespace, outside other classes
public static class SessionExtensions
{
    // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        // This helper function simply serializes theobject to JSON and stores it as a string in session
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
       
    // generic type T is a stand-in indicating that we need to specify the type on retrieval
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        string value = session.GetString(key);
        // Upon retrieval the object is deserialized based on the type we specified
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}


If we copy this code into our project's namespace we can use it from anywhere we want.

// *Inside a controller method*
 
List<object> NewList = new List<object>();
 
HttpContext.Session.SetObjectAsJson("TheList", NewList);
  
// Notice that we specify the type ( List ) on retrieval
List<object> Retrieve = HttpContext.Session.GetObjectFromJson<List<object>>("TheList");


/
/
/
/
/


// TempData *//
using Microsoft.AspNetCore.Http;
 
// Other code
public IActionResult Method()
{
    TempData["Variable"] = "Hello World";
    return RedirectToAction("OtherMethod");
}
public IActionResult OtherMethod()
{
    Console.WriteLine(TempData["Variable"]);
    // writes "Hello World" if redirected to from Method()
}
// *end TempData //




// Candyman Boiler Plate* //
yo candyman <your app name>
dotnet restore
// *end Candyman Boiler Plate //




// ApiCaller.cs* //
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
 
namespace YourNamespace
{
    public class WebRequest
    {
        // The second parameter is a function that returns a Dictionary of string keys to object values.
        // If an API returned an array as its top level collection the parameter type would be "Action>"
        public static async Task GetPokemonDataAsync(int PokeId, Action<Dictionary<string, object>> Callback)
        {
            // Create a temporary HttpClient connection.
            using (var Client = new HttpClient())
            {
                try
                {
                    Client.BaseAddress = new Uri($"http://pokeapi.co/api/v2/pokemon/{PokeId}");
                    HttpResponseMessage Response = await Client.GetAsync(""); // Make the actual API call.
                    Response.EnsureSuccessStatusCode(); // Throw error if not successful.
                    string StringResponse = await Response.Content.ReadAsStringAsync(); // Read in the response as a string.
                     
                    // Then parse the result into JSON and convert to a dictionary that we can use.
                    // DeserializeObject will only parse the top level object, depending on the API we may need to dig deeper and continue deserializing
                    Dictionary<string, object> JsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(StringResponse);
                     
                    // Finally, execute our callback, passing it the response we got.
                    Callback(JsonResponse);
                }
                catch (HttpRequestException e)
                {
                    // If something went wrong, display the error.
                    Console.WriteLine($"Request exception: {e.Message}");
                }
            }
        }
    }
}

*** in YourController.cs ***
[HttpGet]
[Route("pokemon/{pokeid}")]
public IActionResult QueryPoke(int pokeid)
{
    var PokeInfo = new Dictionary<string, object>();
    WebRequest.GetPokemonDataAsync(pokeid, ApiResponse =>
        {
            PokeInfo = ApiResponse;
        }
    ).Wait();
    // Other code
}
*** out ***

// *end ApiCaller.cs //




// DEPLOYMENT* //
*** in web.config ***
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <system.webServer>
        <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModule" resourceType="Unspecified"/>
        </handlers>
        <aspNetCore processPath="%LAUNCHER_PATH%" arguments="%LAUNCHER_ARGS%" stdoutLogEnabled="false" stdoutLogFile=".\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\logs\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\stdout" forwardWindowsAuthToken="false"/>
    </system.webServer>
</configuration>
copy<div id="copy-toolbar-container"><span id="copy-toolbar" style="cursor: pointer; position: absolute; top: 361.465px; right: 35px; padding: 0px 3px; background: rgba(224, 224, 224, 0.2); box-shadow: rgba(0, 0, 0, 0.2) 0px 2px 0px 0px; color: rgb(187, 187, 187); border-radius: 0.5em; font-size: 0.8em;">copy</span></div>
*** out ***


*** in Program.cs ***
using System.IO;
using Microsoft.AspNetCore.Hosting;
 
namespace YourNamespace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
                // New Use method
                .UseIISIntegration()
                .Build();
            host.Run();
        }
    }
}
*** out ***

// *end DEPLOYMENT //





// ******************************************** DAPPER ******************************************** //
// ******************************************** DAPPER ******************************************** //
// ******************************************** DAPPER ******************************************** //
// ******************************************** DAPPER ******************************************** //
// ******************************************** DAPPER ******************************************** //


// Startup* //
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using lostinthewoods.Factory;

namespace lostinthewoods
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSession();
            services.Configure<MySqlOptions>(Configuration.GetSection("DBInfo"));
            services.AddScoped<TrailFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();
        }

        public IConfiguration Configuration { get; private set; }
 
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

    }
}
// *end Startup //



// Controller* //
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using lostinthewoods.Models;
using lostinthewoods.Factory;

namespace lostinthewoods.Controllers
{
    public class TrailController : Controller
    {
        private readonly TrailFactory trailFactory;
        public TrailController(TrailFactory _trailfactory)
        {
            trailFactory = _trailfactory;
        }


        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.allTrails = trailFactory.FindAll();
            return View();
        }


        [HttpGet]
        [Route("addTrail")]
        public IActionResult AddTrail()
        {
            
            return View("newtrail");
        }


        [HttpPost]
        [Route("createTrail")]
        public IActionResult createTrail(Trail trail)
        {
            if(ModelState.IsValid)
            {
                trailFactory.Add(trail);
                return RedirectToAction("Index");
            }
            return View("newtrail");
        } 
        [HttpGet]
        [Route("trails/{id}")]  
        public IActionResult viewTrail(int id)
        {
            ViewBag.trail = trailFactory.FindByID(id);
            return View("trailinfo");
        }
    }
}
// Controller* // 



// Model* //
using System.ComponentModel.DataAnnotations;
 
namespace lostinthewoods.Models
{
    public abstract class BaseEntity {}
    public class Trail : BaseEntity
    {
        [Required]
        [MinLength(1)]
        public string name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage="Description must be at least 10 characters")]
        public string desc { get; set; }


        [Required(ErrorMessage = "Length field is required")]
        public float length { get; set; }
 
        [Required(ErrorMessage = "Elevation change field is required")]
        public int elevation { get; set; }
 
        [Required(ErrorMessage = "Longitude field is required")]
        public string longitude { get; set; }

        [Required(ErrorMessage = "Latitude field is required")]
        public string latitude { get; set; }
    }
}
// *end Model //




// Factory* //
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using lostinthewoods.Models;
using Microsoft.Extensions.Options;


namespace lostinthewoods.Factory
{
    public class TrailFactory : IFactory<Trail>
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
        public TrailFactory(IOptions<MySqlOptions> config)
        {
            MySqlConfig = config;
        }
        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(MySqlConfig.Value.ConnectionString);
            }
        }

        public void Add(Trail item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO trails (name, desc, elevation, length, longitude, latitude) VALUES (@name, @desc, @elevation, @length, @longitude, @latitude)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

        public List<Trail> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails").ToList();
            }
        }

        public Trail FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

    }
}
// *end Factory //