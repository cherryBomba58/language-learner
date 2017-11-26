﻿using System;
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
    public class LearnableController : Controller
    {
        private readonly LearnableManager _manager;

        public LearnableController(LearnableManager manager)
        {
            _manager = manager;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<LearnableModel> learneables = _manager.GetLearnables();
            return Json(learneables);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IEnumerable<LearnableModel> learnables = _manager.GetLearnablesByCourseId(id);
            return Json(learnables);
        }

        // POST api/values
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public IActionResult Post([FromBody]LearnableModel model)
        {
            _manager.AddLearnable(model);
            return Json("OK");
        }

        // PUT api/values/5
        [Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]LearnableModel model)
        {
            _manager.PutLearnable(id, model);
            return Json("OK");
        }

        // DELETE api/values/5
        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _manager.DeleteLearnable(id);
            return Json("OK");
        }
    }
}
