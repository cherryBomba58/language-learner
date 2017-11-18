using LanguageLearner.BLL.Models;
using LanguageLearner.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageLearner.BLL.Managers
{
    public class CourseManager
    {
        private readonly LanguageLearnerContext _context;

        // Dependency injection! No need to use using, 
        // ASP.NET Core will care of disposing of context
        public CourseManager(LanguageLearnerContext context)
        {
            _context = context;
        }

        public IEnumerable<CourseModel> GetCourses()
        {
            return _context.Courses.Select(c => new CourseModel
            {
                CourseID = c.CourseID,
                Name = c.Name
            }).ToList();
        }
    }
}
