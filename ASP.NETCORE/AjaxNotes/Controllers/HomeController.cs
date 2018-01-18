using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace AjaxNotes.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {

            // Display all notes from db
            ViewBag.AllNotes = DbConnector.Query("SELECT * FROM notes_table");
            return View();
        }

        [HttpPost]
        [Route("newNote")]
        public IActionResult newNote(string title, string description) 
        {
            string Title = '"' + title + '"' ;
            string Description = '"' + description + '"';
            string query = $"INSERT INTO notes_table (title, description) VALUES ({Title}, {Description})";
            DbConnector.Execute(query);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("editNote")]
        public IActionResult editNote(int id, string title, string description) 
        {
            // int ID = id;
            string Title = '"' + title + '"';
            string Description = '"' + description + '"';
            string EditQuery = $"UPDATE notes_table SET title = {Title}, description = {Description} WHERE id = id";
            System.Console.WriteLine(EditQuery);
            DbConnector.Execute(EditQuery);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            int ID = id ;
            DbConnector.Execute($"DELETE FROM notes_table WHERE id = {ID}");
            return RedirectToAction("Index");
        }
    }
}
