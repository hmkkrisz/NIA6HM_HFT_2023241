using System;
using Microsoft.EntityFrameworkCore;
using NIA6HM_HFT_2023241.Models;


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

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            if (!optionsbuilder.IsConfigured)
            {
                optionsbuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\blog.mdf;Integrated Security=True;MultipleActiveResultSets=true");
                    

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Author a1 = new Author() { AuthorId = 1, Name = "John Doe" };
            Author a2 = new Author() { AuthorId = 2, Name = "Jane Smith" };
            Author a3 = new Author() { AuthorId = 3, Name = "Michael Johnson" };
            Author a4 = new Author() { AuthorId = 4, Name = "Emily Davis" };
            Author a5 = new Author() { AuthorId = 5, Name = "Robert White" };

            
            Article article1 = new Article() { ArticleId = 1, Title = "Galactic Odyssey: Beyond the Stars", Category = "Sci-Fi", Likes = 14};
            Article article2 = new Article() { ArticleId = 2, Title = "Cybernetic Chronicles: Rise of the Machines", Category = "Sci-Fi", Likes = 69 };
            Article article3 = new Article() { ArticleId = 3, Title = "Realm of the Mystic Dragons", Category = "Fantasy", Likes = 46 };
            Article article4 = new Article() { ArticleId = 4, Title = "Enchanted Kingdoms: The Elven Legacy", Category = "Fantasy", Likes = 88 };
            Article article5 = new Article() { ArticleId = 5, Title = "Sorcerer's Apprentice: Magical Realms", Category = "Fantasy", Likes = 27 };
            Article article6 = new Article() { ArticleId = 6, Title = "Lost Treasures of the Amazon", Category = "Adventure", Likes = 23 };
            Article article7 = new Article() { ArticleId = 7, Title = "Pirate's Cove: Tales of the High Seas", Category = "Adventure", Likes = 100 };
            Article article8 = new Article() { ArticleId = 8, Title = "Whispers in the Moonlit Mansion", Category = "Mistery", Likes = 76 };
            Article article9 = new Article() { ArticleId = 9, Title = "Cryptic Conundrums: Detective Chronicles", Category = "Mistery", Likes = 75 };
            Article article10 = new Article() { ArticleId = 10, Title = "Epic Tales from Ancient Empires", Category = "Historical", Likes = 12 };


            Comment c0 = new Comment() { CommentId = 1, Text = "Some words were incorrect." };
            Comment c1 = new Comment() { CommentId = 2, Text = "Lorem ipsum dolor sit amet." };
            Comment c2 = new Comment() { CommentId = 3, Text = "Great article, loved it!" };
            Comment c3 = new Comment() { CommentId = 4, Text = "Fantastic reading." };
            Comment c4 = new Comment() { CommentId = 5, Text = "Thanks for sharing this stuff!" };
            Comment c5 = new Comment() { CommentId = 6, Text = "Waste of tiiiiiiiiiiiime...." };
            Comment c6 = new Comment() { CommentId = 7, Text = "Lorem ipsum." };
            Comment c7 = new Comment() { CommentId = 8, Text = "Dolor sit amet." };
            Comment c8 = new Comment() { CommentId = 9, Text = "This is pure SH!T." };
            Comment c9 = new Comment() { CommentId = 10, Text = "Good words used here!" };
            Comment c10 = new Comment() { CommentId = 11, Text = "Excellent content, I enjoyed it!" };
            Comment c11 = new Comment() { CommentId = 12, Text = "I have mixed feelings about this." };
            Comment c12 = new Comment() { CommentId = 13, Text = "Can't wait for the next article!" };
            Comment c13 = new Comment() { CommentId = 14, Text = "This article changed my perspective." };
            Comment c14 = new Comment() { CommentId = 15, Text = "Well-written and thought-provoking." };


            article1.AuthorId = a1.AuthorId;
            article2.AuthorId = a2.AuthorId;
            article3.AuthorId = a3.AuthorId;
            article4.AuthorId = a4.AuthorId;
            article5.AuthorId = a5.AuthorId;
            article6.AuthorId = a3.AuthorId;
            article7.AuthorId = a1.AuthorId;
            article8.AuthorId = a4.AuthorId;
            article9.AuthorId = a1.AuthorId;
            article10.AuthorId = a5.AuthorId;

            c0.ArticleId = article1.ArticleId;
            c1.ArticleId = article2.ArticleId;
            c2.ArticleId = article3.ArticleId;
            c3.ArticleId = article4.ArticleId;
            c4.ArticleId = article5.ArticleId;

            c5.ArticleId = article6.ArticleId;
            c6.ArticleId = article6.ArticleId;
            c7.ArticleId = article6.ArticleId;

            c8.ArticleId = article7.ArticleId;
            c9.ArticleId = article7.ArticleId;
            c10.ArticleId = article7.ArticleId;
            c11.ArticleId = article7.ArticleId;

            c12.ArticleId = article8.ArticleId;
            c13.ArticleId = article9.ArticleId;
            c14.ArticleId = article10.ArticleId;

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasOne(article => article.Author)
                    .WithMany(author => author.Articles)
                    .HasForeignKey(article => article.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(comment => comment.Article)
                    .WithMany(article => article.Comments)
                    .HasForeignKey(comment => comment.ArticleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Author>().HasData(a1, a2, a3, a4, a5);
            modelBuilder.Entity<Article>().HasData(article1, article2, article3, article4, article5, article6, article7, article8, article9, article10);
            modelBuilder.Entity<Comment>().HasData(c0, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14);
        }
    }
}