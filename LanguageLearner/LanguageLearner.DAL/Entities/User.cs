using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearner.DAL.Entities
{
    public class User
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public List<CourseResults> CourseResultsList { get; set; }
        public List<LearnableResults> LearnableResultsList { get; set; }
    }
}
