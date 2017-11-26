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
