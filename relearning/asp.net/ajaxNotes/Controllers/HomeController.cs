using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DbConnection;

namespace ajaxNotes.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.AllNotes = DbConnector.Query("SELECT * FROM notes");
            return View();
        }

        [HttpPost]
        [Route("NewNote")]
        public IActionResult NewNote(string title, string description, string author)
        {
            string SqlTitle = "'" + title.Trim() + "'";
            string SqlDesc = "'" + description.Trim() + "'";
            string SqlAuthor = "'" + author.Trim() + "'";
            DbConnector.Execute($"INSERT INTO notes (author, title, description, created_at, updated_at) VALUES ({SqlAuthor}, {SqlTitle}, {SqlDesc}, NOW(), NOW())");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("UpdateNote/{id}")]
        public IActionResult UpdateNote(string title, string description, int id)
        {
            string SqlTitle = "'" + title.Trim() + "'";
            string SqlDesc = "'" + description.Trim() + "'";
            DbConnector.Execute($"UPDATE notes SET title = {SqlTitle}, description = {SqlDesc} WHERE id = {id}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            DbConnector.Execute($"DELETE FROM notes WHERE id = {id}");
            return RedirectToAction("Index");
        }
    }
}
