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
        // A CourseModel nem tartalmazza  Learnable-k listáját,
        // a Learnable-ket viszont érdemes másik kontrollerben (LearnableController),
        // másik Post-ban küldeni az adatbázis felé, akár Learnable-k tömbjét is
        // (az is lehet külön LearnableListModel, amely csak  Learnable-k listáját tartalmazza),
        // és azoknak megadni a kurzusuk kulcsát, az ő külső kulcsukként.
        // A CourseModel id-jára sincs most szükség, csak szemléltetés, hogy modelleket kell itt is alkalmazni.
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
