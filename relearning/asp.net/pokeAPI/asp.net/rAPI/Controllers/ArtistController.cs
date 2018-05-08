using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace MusicApi.Controllers {

    
    public class ArtistController : Controller {

        private List<Artist> allArtists;

        public ArtistController() {
            allArtists = JsonToFile<Artist>.ReadJson();
        }

        //This method is shown to the user navigating to the default API domain name
        //It just display some basic information on how this API functions
        [Route("")]
        [HttpGet]
        public string Index() {
            //String describing the API functionality
            string instructions = "Welcome to the Music API~~\n========================\n";
            instructions += "    Use the route /artists/ to get artist info.\n";
            instructions += "    End-points:\n";
            instructions += "       *Name/{string}\n";
            instructions += "       *RealName/{string}\n";
            instructions += "       *Hometown/{string}\n";
            instructions += "       *GroupId/{int}\n\n";
            instructions += "    Use the route /groups/ to get group info.\n";
            instructions += "    End-points:\n";
            instructions += "       *Name/{string}\n";
            instructions += "       *GroupId/{int}\n";
            instructions += "       *ListArtists=?(true/false)\n";
            return instructions;
        }

        [HttpGet]
        [Route("artists")]
        public JsonResult displayAllArtists()
        {
            var allArtistsQuery = allArtists.OrderBy(a => a.ArtistName);
            return Json(allArtistsQuery);
        }

        [HttpGet]
        [Route("artists/name/{name}")]
        public JsonResult findArtist(string name)
        {
            var artist = allArtists.Where(a => a.ArtistName == name);
            return Json(artist);
        }
        [HttpGet]
        [Route("artists/realname/{name}")]
        public JsonResult findArtistRealName(string name)
        {
            var artist = allArtists.Where(a => a.RealName == name);
            foreach(var person in artist)
            {
                System.Console.WriteLine(person.RealName);
            }
            return Json(artist);
        }

        [HttpGet]
        [Route("artists/hometown/{town}")]
        public JsonResult artistsHometown(string town)
        {
            var artist = allArtists.Where(a => a.Hometown == town);
            return Json(artist);
        }

        [HttpGet]
        [Route("artists/groupid/{id}")]
        public JsonResult groupId(int id)
        {
            var artist = allArtists.Where(a => a.GroupId == id);
            return Json(artist);
        }
    }
}