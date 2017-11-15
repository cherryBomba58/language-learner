using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearner.DAL.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public List<CourseResults> CourseResultsList { get; set; }
        public List<LearnableResults> LearnableResultsList { get; set; }
    }
}
