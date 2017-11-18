using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LanguageLearner.BLL.Managers;
using LanguageLearner.BLL.Models;

namespace LanguageLearner.WebAPI.Controllers
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
