using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearner.BLL.Models
{
    public class AnswerModel
    {
        public int LearnableID { get; set; }
        public string GoodAnswer { get; set; }
        public string Answer { get; set; }
        public bool Right { get; set; }
    }
}
