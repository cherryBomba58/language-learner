using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LanguageLearner.BLL.Managers;
using LanguageLearner.BLL.Models;
using Microsoft.AspNetCore.Authorization;

namespace LanguageLearner.ANG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ResultsController : Controller
    {
        private readonly ResultsManager _manager;

        public ResultsController(ResultsManager manager)
        {
            _manager = manager;
        }

        // GET api/values
        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<UsersCourseResultModel> usersCourseResults = _manager.GetUsersCourseResults();
            return Json(usersCourseResults);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            IEnumerable<UsersCourseResultModel> usersCourseResults = _manager.GetUsersCourseResults(id);
            return Json(usersCourseResults);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ResultsModel model)
        {
            _manager.AddResults(model, User.Identity.Name);
            return Json("OK");
        }
    }
}
