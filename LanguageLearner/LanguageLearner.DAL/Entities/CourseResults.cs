using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearner.DAL.Entities
{
    public class CourseResults
    {
        public string UserID { get; set; }
        public int CourseID { get; set; }
        public DateTime Date { get; set; }
        public int Points { get; set; }

        public User User { get; set; }
    }
}
