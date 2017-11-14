using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearner.DAL.Entities
{
    public class LearnableResults
    {
        public string UserID { get; set; }
        public int LearnableID { get; set; }
        public int RightAnswerNum { get; set; }
        public int WrongAnswerNum { get; set; }
    }
}
