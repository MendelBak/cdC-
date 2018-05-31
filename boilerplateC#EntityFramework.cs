// THIS BOILERPLATE FILE IS FOR A PROJECT THAT USES THE ENTITY CORE FRAMEWORK.

/************************ Commands *************************/
// To start a new web app with MVC, create a folder, enter it. then run:
dotnet new MVC
// To restore a folder to make dependencies run:
dotnet restore
// To run a C# app:
dotnet run
// If Microsoft.DotNet.Watcher.Tools is included in your ProjectName.csproj file (will watch for file changes and recompile automatically):
<DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="2.0.0" />
dotnet watch run
// Set development environment variable for developer exception/error messages in your terminal:
export ASPNETCORE_ENVIRONMENT=Development
// Install MySQL in your project
dotnet add package MySql.Data -v 8.0.11-*


// After you have created a new mvc app, create a few new directories and files following this scaffolding strategy //

// (root)
// .vscode (Autogenerated. Debugger files)
// > bin (Autogenerated)
// > Controllers (Create this directory)
//   > HomeController.cs
// > Models (Classes and database context files go here)
//   > DbContext.cs
//   > User.cs 
// > obj (Autogenerated)
// > Views (Create this directory)
//   > Home (Must match the controller name from above)
//     > index.cshtml
//   > Shared (This is the last place the compiler will look for views. It is for views that are shared between multiple controllers)
// > wwwroot (autogenerated. This contains your static files (images, css, js).)
//   > css (Create this directory)
//     > style.css
//   > img (Create this directory)
//     > img1.jpg
//   > js (Create this directory)
//     > script.js
// > appsettings.json (This is where you have your DB connection string. It is read by the DbConnection file. It is kept in this file in order to maintain security.)
// > Properties (Create this directory)
//   > MySqlOptions.cs (This contains a model for SQL options. Only create this if you need to access the SQL connection string in your other project in a place other than the appsettings.json file(which connects to the DB))


// END FILE STRUCTURE //

/************************ Configuring your files*************************/
// DONT FORGET TO CHANGE NAMESPACES, DB NAMES, AND PROJECT NAMES!

// Startup.cs (contains "Using" statements and is where you enable services like session, MVC, DbContext, and other tools.):

// appsettings.json (Is where you define your DB connection string.. Add this file to .gitignore to prevent it from being pushed to remote repo)

// This file obviously uses a PostgreSQL database. You can change that.
{
  "DBInfo": {
    "Name": "PostGresConnect",
    "ConnectionString": "server=localhost;userId=postgres;password=postgres;port=5432;database=tckr_db;"
  },
  "tools": {
    "Microsoft.EntityFrameworkCore.Tools": "1.0.0-preview2-final"
  },
  "dependencies": {
    "Microsoft.Extensions.Configuration.Json": "1.0.0",
    "Npgsql.EntityFrameworkCore.PostgreSQL": "1.0.0-*",
    "Microsoft.EntityFrameworkCore.Design": {
      "version": "1.0.0-preview2-final",
      "type": "build"
    }
  }
}

// END appsettings.json