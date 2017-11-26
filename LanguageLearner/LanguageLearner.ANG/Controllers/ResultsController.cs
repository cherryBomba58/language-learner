using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LanguageLearner.BLL.Managers;
using LanguageLearner.BLL.Models;

namespace LanguageLearner.ANG.Controllers
{
    [Route("api/[controller]")]
    public class ResultsController : Controller
    {
        private readonly ResultsManager _manager;

        public ResultsController(ResultsManager manager)
        {
            _manager = manager;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
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
