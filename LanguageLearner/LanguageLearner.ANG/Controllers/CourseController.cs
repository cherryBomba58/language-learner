﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LanguageLearner.BLL.Managers;
using LanguageLearner.BLL.Models;

namespace LanguageLearner.ANG.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly CourseManager _manager;

        public CourseController(CourseManager manager)
        {
            _manager = manager;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<CourseModel> courses = _manager.GetCourses();
            return Json(courses);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            CourseModel course = _manager.GetCourseById(id);
            return Json(course);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CourseModel model)
        {
            _manager.AddCourse(model);
            return Json("OK");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CourseModel model)
        {
            _manager.PutCourse(id, model);
            return Json("OK");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _manager.DeleteCourse(id);
            return Json("OK");
        }
    }
}
