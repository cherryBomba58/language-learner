using LanguageLearner.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearner.DAL.Context
{
    public class LanguageLearnerContext : IdentityDbContext<User>
    {
        public LanguageLearnerContext(DbContextOptions<LanguageLearnerContext> options)
            : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseResults> CourseResults { get; set; }
        public DbSet<Learnable> Learnables { get; set; }
        public DbSet<LearnableResults> LearnableResults { get; set; }
        public DbSet<User> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CourseResults>().ToTable("CourseResults")
                .HasKey(c => new { c.UserID, c.CourseID, c.Date});
            modelBuilder.Entity<LearnableResults>().ToTable("LearnableResults")
                .HasKey(l => new { l.UserID, l.LearnableID });
        }

    }
}
