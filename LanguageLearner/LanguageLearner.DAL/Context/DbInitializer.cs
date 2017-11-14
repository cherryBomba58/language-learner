using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageLearner.DAL.Context
{
    public static class DbInitializer
    {
        public static void Initialize(LanguageLearnerContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Courses.Any())
            {
                return;   // DB has been seeded
            }

            var courses = new Course[]
            {
                new Course{Name="Family"},
                new Course{Name="Work"}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();
            

            var learnables = new Learnable[]
            {
                new Learnable{English="Father", Hungarian="Apa",WordClass="Noun", CourseID=1},
                new Learnable{English="Mother", Hungarian="Anya",WordClass="Noun", CourseID=1},
                new Learnable{English="Job", Hungarian="Foglalkozás",WordClass="Noun", CourseID=2},
                new Learnable{English="Boss", Hungarian="Főnök",WordClass="Noun", CourseID=2}
            };
            foreach (Learnable l in learnables)
            {
                context.Learnables.Add(l);
            }
            context.SaveChanges();
        }
    }
}
