using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProgrammingVocabulary.Models;

namespace ProgrammingVocabulary.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)

        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Language> Language { get; set; }       
        public DbSet<Vocabulary> Vocabulary { get; set; }
        public DbSet<UserVocabulary> UserVocabulary { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
    }
}
