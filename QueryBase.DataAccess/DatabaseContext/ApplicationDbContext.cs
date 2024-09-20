using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QueryBase.Models;

namespace QueryBase.DataAccess.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }



        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<VoteAnswer> VoteAnswers { get; set; }
        public DbSet<VoteQuestion> VoteQuestions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = new Guid("02629b35-b8ef-4658-9cee-c0ec06da244f"), CategoryName = "Python" },
                new Category() { Id = new Guid("94276b07-a9dc-4460-9642-ab604c7cf2ff"), CategoryName = "C#" },
                new Category() { Id = new Guid("65aac0d2-e0fa-4e1f-a5d5-ada0913d4eee"), CategoryName = "C++" },
                new Category() { Id = new Guid("6c65ab2b-53e5-4eb3-8911-b75687e542c3"), CategoryName = "Java" }
            );
        }
    }
}
