using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace MusicApi.Controllers {
    public class GroupController : Controller {
        List<Group> allGroups {get; set;}
        public GroupController() {
            allGroups = JsonToFile<Group>.ReadJson();
        }

        [HttpGet]
        [Route("groups")]
        public JsonResult displayAllGroups()
        {
            var groups = allGroups.OrderBy(g => g.GroupName);
            return Json(groups);
        }

        [HttpGet]
        [Route("groups/name/{name}")]
        public JsonResult findGroup(string name)
        {
            var group = allGroups.Where(g => g.GroupName == name);
            return Json(group);
        }

        [HttpGet]
        [Route("groups/id/{id}")]
        public JsonResult groupId(int id)
        {
            var group = allGroups.Where(g => g.Id == id);
            return Json(group);
        }

    }
}