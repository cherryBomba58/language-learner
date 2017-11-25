using LanguageLearner.BLL.Models;
using LanguageLearner.DAL.Context;
using LanguageLearner.DAL.Entities;
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

        public CourseModel GetCourseById(int id)
        {
            var course = _context.Courses.Single(c => c.CourseID == id);
            return new CourseModel
            {
                CourseID = course.CourseID,
                Name = course.Name
            };
        }

        public void AddCourse(CourseModel model)
        {
            // It will get ID automatically in the DB
            var course = new Course
            {
                Name = model.Name
            };
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void PutCourse(int id, CourseModel model)
        {
            // Search by the given ID the modify
            var course = _context.Courses.Single(c => c.CourseID == id);
            course.Name = model.Name;
            _context.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            // Search by the given ID then delete
            var course = _context.Courses.Single(c => c.CourseID == id);
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }
    }
}
