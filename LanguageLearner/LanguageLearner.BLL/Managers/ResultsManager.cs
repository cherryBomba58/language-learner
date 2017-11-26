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
    public class ResultsManager
    {
        private readonly LanguageLearnerContext _context;

        // Dependency injection! No need to use using, 
        // ASP.NET Core will care of disposing of context
        public ResultsManager(LanguageLearnerContext context)
        {
            _context = context;
        }

        public IEnumerable<UsersCourseResultModel> GetUsersCourseResults()
        {
            List<UsersCourseResultModel> results = new List<UsersCourseResultModel>();
            foreach(var user in _context.Users.Include(u => u.CourseResultsList))
            {
                results.AddRange(user.CourseResultsList.Select(r => new UsersCourseResultModel
                {
                    FullName = user.FullName,
                    UserID = user.Id,
                    CourseName = _context.Courses.Single(c => c.CourseID == r.CourseID).Name,
                    Date = r.Date,
                    Points = r.Points
                }));
            }
            return results;
        }

        public List<UsersCourseResultModel> GetUsersCourseResults(String id)
        {
            List<UsersCourseResultModel> results = new List<UsersCourseResultModel>();
            var user = _context.Users.Include(u => u.CourseResultsList).Single(u => u.Id == id);
            results.AddRange(user.CourseResultsList.Select(r => new UsersCourseResultModel
            {
                FullName = user.FullName,
                UserID = user.Id,
                CourseName = _context.Courses.Single(c => c.CourseID == r.CourseID).Name,
                Date = r.Date,
                Points = r.Points
            }));
            return results;
        }

        public void AddResults(ResultsModel model, string username)
        {
            var courseResult = new CourseResults
            {
                CourseID = model.CourseID,
                Date = DateTime.Now,
                UserID = username,
                Points = model.AnswerList.Count(a => a != null && a.Right == true)
            };
            _context.CourseResults.Add(courseResult);
            _context.SaveChanges();

            foreach(var answer in model.AnswerList)
            {
                if(answer != null)
                {
                    var learnableResult = _context.LearnableResults.Find(username, answer.LearnableID);
                    if (learnableResult == null)
                    {
                        learnableResult = new LearnableResults
                        {
                            LearnableID = answer.LearnableID,
                            UserID = username,
                            RightAnswerNum = (answer.Right == true) ? 1 : 0,
                            WrongAnswerNum = (answer.Right == false) ? 1 : 0
                        };
                        _context.LearnableResults.Add(learnableResult);
                    }
                    else
                    {
                        if (answer.Right == true)
                        {
                            learnableResult.RightAnswerNum += 1;
                        }
                        else
                        {
                            learnableResult.WrongAnswerNum += 1;
                        }
                    }
                    _context.SaveChanges();
                }
            }
        }
    }
}
