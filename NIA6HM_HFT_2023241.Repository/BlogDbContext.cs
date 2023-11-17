using Microsoft.EntityFrameworkCore;
using NIA6HM_HFT_2023241.Models;
using System;

namespace NIA6HM_HFT_2023241.Repository
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogDbContext()
        {
            this.Database.EnsureCreated();
        }
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }
    }
}
