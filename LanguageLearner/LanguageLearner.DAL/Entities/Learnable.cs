using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearner.DAL
{
    public class Learnable
    {
        public int LearnableID { get; set; }
        public int CourseID { get; set; }
        public string English { get; set; }
        public string Hungarian { get; set; }
        public string WordClass { get; set; } = null;
        public string PictureUrl { get; set; } = null;
    }
}
