using System;
using System.Collections.Generic;

namespace LanguageLearner.DAL.Entities
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public List<Learnable> LearnableList { get; set; }
    }
}
