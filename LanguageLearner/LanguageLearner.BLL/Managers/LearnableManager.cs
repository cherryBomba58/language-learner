using LanguageLearner.BLL.Models;
using LanguageLearner.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageLearner.BLL.Managers
{
    public class LearnableManager
    {
        private readonly LanguageLearnerContext _context;

        // Dependency injection! No need to use using, 
        // ASP.NET Core will care of disposing of context
        public LearnableManager(LanguageLearnerContext context)
        {
            _context = context;
        }

        public IEnumerable<LearnableModel> GetLearnables(int courseID)
        {
            return _context.Courses
                .Include(c => c.LearnableList)
                .Single(c => c.CourseID == courseID)
                .LearnableList
                .Select(l => new LearnableModel
                {
                    CourseID = l.CourseID,
                    English = l.English,
                    Hungarian = l.Hungarian,
                    LearnableID = l.LearnableID,
                    PictureUrl = l.PictureUrl,
                    WordClass = l.WordClass
                }).ToList();
        }
    }
}
