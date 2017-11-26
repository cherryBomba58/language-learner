using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearner.BLL.Models
{
    public class UsersCourseResultModel
    {
        public string FullName { get; set; }
        public string UserID { get; set; }
        public string CourseName { get; set; }
        public DateTime Date { get; set; }
        public int Points { get; set; }
    }
}
