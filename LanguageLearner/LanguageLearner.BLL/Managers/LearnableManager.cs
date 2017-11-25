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
    public class LearnableManager
    {
        private readonly LanguageLearnerContext _context;

        // Dependency injection! No need to use using, 
        // ASP.NET Core will care of disposing of context
        public LearnableManager(LanguageLearnerContext context)
        {
            _context = context;
        }

        public IEnumerable<LearnableModel> GetLearnables()
        {
            return _context.Learnables.Select(l => new LearnableModel
            {
                LearnableID = l.LearnableID,
                CourseID = l.CourseID,
                English = l.English,
                Hungarian = l.Hungarian,
                PictureUrl = l.PictureUrl,
                WordClass = l.WordClass
            }).ToList();
        }

        public IEnumerable<LearnableModel> GetLearnablesByCourseId(int courseID)
        {
            return _context.Courses
                .Include(c => c.LearnableList)
                .Single(c => c.CourseID == courseID)
                .LearnableList
                .Select(l => new LearnableModel
                {
                    LearnableID = l.LearnableID,
                    CourseID = l.CourseID,
                    English = l.English,
                    Hungarian = l.Hungarian,
                    PictureUrl = l.PictureUrl,
                    WordClass = l.WordClass
                }).ToList();
        }

        public void AddLearnable(LearnableModel model)
        {
            // It will get ID automatically in the DB
            var learnable = new Learnable
            {
                CourseID = model.CourseID,
                English = model.English,
                Hungarian = model.Hungarian,
                PictureUrl = model.PictureUrl,
                WordClass = model.WordClass
            };
            _context.Learnables.Add(learnable);
            _context.SaveChanges();
        }

        public void PutLearnable(int id, LearnableModel model)
        {
            // Search by the given ID the modify
            var learnable = _context.Learnables.Single(l => l.LearnableID == id);
            learnable.CourseID = model.CourseID;
            learnable.English = model.English;
            learnable.Hungarian = model.Hungarian;
            learnable.PictureUrl = model.PictureUrl;
            learnable.WordClass = model.WordClass;
            _context.SaveChanges();
        }

        public void DeleteLearnable(int id)
        {
            // Search by the given ID then delete
            var learnable = _context.Learnables.Single(l => l.LearnableID == id);
            _context.Learnables.Remove(learnable);
            _context.SaveChanges();
        }
    }
}
