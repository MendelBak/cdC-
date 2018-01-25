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

// appsettings.json (SQL Version)* //
{
    "DBInfo":
    {
        "Name": "MySQLconnect",
        "ConnectionString": "server=localhost;userid=root;password=root;port=8889;database=mydb;SslMode=None"
    }
}
// *end appsettings.json //


// DbConnection.cs* used without Dapper//
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
    // <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.3" /> Only use this package when using netcoreapp2.0
    <PackageReference Include="Microsoft.AspNetCore.Identity" Version="2.0.1" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.0.1" />
    <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="1.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Session" Version="2.0.1" />
    <PackageReference Include="MySql.Data.EntityFrameworkCore" Version="7.0.7-*" />
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


// MODEL User.cs* //
using System;
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



// Candyman Boiler Plate* //
yo candyman <your app name>
dotnet restore
// *end Candyman Boiler Plate //





// ******************************************** DAPPER ******************************************** //
// ******************************************** DAPPER ******************************************** //
// ******************************************** DAPPER ******************************************** //
// ******************************************** DAPPER ******************************************** //
// ******************************************** DAPPER ******************************************** //


// appsettings.json* //
{
    "DBInfo":
    {
        "Name": "MySQLconnect",
        "ConnectionString": "server=localhost;userid=root;password=root;port=8889;database=mydb;SslMode=None"
    }
}
// *end appsettings.json //

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
using LostInTheWoods.Models;
using Microsoft.AspNetCore.Mvc;
using LostInTheWoods.Factory; //Need to include reference to new Factory Namespace
namespace LostInTheWoods.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrailFactory trailFactory;
        public HomeController()
        {
            //Instantiate a UserFactory object that is immutable (READONLY)
            //This establishes the initial DB connection for us.
            trailFactory = new TrailFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            //We can call upon the methods of the userFactory directly now.
            ViewBag.AllTrails = trailFactory.FindAll();
            return View();
        }
        

        [HttpGet]
        [Route("newtrail")]
        public IActionResult NewTrail()
        {
            return View("newtrail");
        }


        [HttpGet]
        [Route("trails/{id}")]
        public IActionResult Trails(int id)
        {
            ViewBag.Trail = trailFactory.FindByID(id);
            return View("trails");
        }

        [HttpPost]
        [Route("SubmitTrail")]
        public IActionResult SubmitTrail(Trail trail)
        {
            trailFactory.Add(trail);
            return RedirectToAction("index");
        }
    }
}

// Controller* // 



// Model* //
using System;
using System.ComponentModel.DataAnnotations;

namespace LostInTheWoods.Models
{
    public abstract class BaseEntity { }

    public class Trail : BaseEntity
    {
        [Key]
        public long id { get; set; }


        [Required]
        [MinLength(4, ErrorMessage = "Trail Name must be at least 4 characters")]
        public string Name { get; set; }


        [Required]
        [MinLength(4, ErrorMessage = "Description must be at least 4 characters")]
        public string Description { get; set; }


        [Required]
        public int Length { get; set; }


        [Required]
        public int ElevationGain { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }
    }
}

// *end Model //




// Factory* //using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using LostInTheWoods.Models;

namespace LostInTheWoods.Factory
{
    public class TrailFactory : IFactory<Trail>
    {
        private string connectionString;
        public TrailFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=lostinthe_db;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(connectionString);
            }
        }


        public void Add(Trail trail)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO trails (name, description, length, elevation_gain, longitude, latitude, created_at, updated_at) VALUES (@Name, @Description, @Length, @ElevationGain, @Longitude, @Latitude, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, trail);
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

// IFactory.cs* //
using LostInTheWoods.Models;
using System.Collections.Generic;
namespace LostInTheWoods.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
    }
}

// *end IFactory.cs //


// ******************************************** ENTITY CORE FRAMEWORK ******************************************** //
// ******************************************** ENTITY CORE FRAMEWORK ******************************************** //
// ******************************************** ENTITY CORE FRAMEWORK ******************************************** //
// ******************************************** ENTITY CORE FRAMEWORK ******************************************** //
// ******************************************** ENTITY CORE FRAMEWORK ******************************************** //


// *end Models/Context.cs //
using Microsoft.EntityFrameworkCore;
using System;


namespace BankAccounts.Models
{
    public class BankContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }
        // First variable should mirror the model class name. Second variable should mirror DB table name. (In PostgreSQL it will create schemas and tables that mirror these variables.) //
        public DbSet<User> Users { get; set; }
    }

}

// *end Models/Context //

// *Add to Startup.cs (Using MySQL) //
using RESTauranter.Models;
using MySQL.Data.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSession();
            services.Configure<MySqlOptions>(Configuration.GetSection("DBInfo"));
            services.AddDbContext<ReviewContext>(options => options.UseMySQL(Configuration["DBInfo:ConnectionString"]));
        }
// *end Additions to Startup.cs //


// *Controller.cs //;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RESTauranter.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RESTauranter.Controllers
{

    public class HomeController : Controller
    {
        private ReviewContext _context;

        public HomeController(ReviewContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // List<Person> AllUsers = _context.Users.ToList();
            return View("Index");
        }
    }

}
// *end Controller.cs //

// * This is what .csproj should look like when using PostgreSQL //
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>netcoreapp1.1</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Dapper" Version="1.50.4" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="1.1.2" />
    <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="1.1.1" />
    <PackageReference Include="Microsoft.AspNetCore.Session" Version="1.1.1" />
    <PackageReference Include="Microsoft.AspNetCore.Server.Kestrel" Version="1.1.1" />
    <PackageReference Include="Microsoft.AspNetCore.Diagnostics" Version="1.1.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="1.1.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.1" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="1.1.2" />
    <PackageReference Include="Microsoft.Extensions.Logging.Console" Version="1.1.1" />
    <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="1.1" />
    <PackageReference Include="Microsoft.AspNetCore.Identity" Version="1.0.1" />
    <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="1.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Server.IISIntegration" Version="1.0.1" />
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="1.1.1" />
  </ItemGroup>
  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="1.0.0" />
  </ItemGroup>
  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.0" />
</ItemGroup>    
</Project>
// *End PostgreSQL version of .csproj //


// appsettings.json (PostgreSQL Version)* //
{
    "DBInfo": 
    {
        "Name": "PostGresConnect",
        "ConnectionString": "server=localhost;userId=root;password=root;port=5432;database=BankAccountDB;"
    },
    "tools": 
    {
        "Microsoft.EntityFrameworkCore.Tools": "1.0.0-preview2-final"
    },
    "dependencies": 
    {
        "Microsoft.Extensions.Configuration.Json": "1.0.0",
        "Npgsql.EntityFrameworkCore.PostgreSQL": "1.0.0-*",
        "Microsoft.EntityFrameworkCore.Design": 
        {
            "version": "1.0.0-preview2-final",
            "type": "build"
        }
    }
}
// *end appsettings.json //


// Startup.cs (when using PostgreSQL* //

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
using Microsoft.EntityFrameworkCore;
// using RESTauranter.Models;

namespace BankAccounts
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSession();
            services.AddDbContext<YourContext>(options => options.UseNpgsql(Configuration["DBInfo:ConnectionString"]));
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
// END Startup.cs (when using PostgreSQL* //