using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearner.BLL.Models
{
    public class ResultsModel
    {
        public int CourseID { get; set; }
        public IEnumerable<AnswerModel> AnswerList { get; set; }
    }
}
