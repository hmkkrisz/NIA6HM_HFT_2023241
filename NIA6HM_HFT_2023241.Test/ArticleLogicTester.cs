using System;
using System.Collections.Generic;
using System.Linq;
using NIA6HM_HFT_2023241.Logic;
using NIA6HM_HFT_2023241.Repository;
using NIA6HM_HFT_2023241.Models;
using Moq;
using NUnit.Framework;
using static NIA6HM_HFT_2023241.Logic.ArticleLogic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace NIA6HM_HFT_2023241.Test
{
    [TestFixture]
    public class ArticleLogicTester
    {
        CommentLogic commentLogic;
        Mock<IRepository<Comment>> mockCommentRepo;
        ArticleLogic articleLogic;   
        Mock<IRepository<Article>> mockArticleRepo;
      

        [SetUp]
        public void Init()
        {
            mockCommentRepo = new Mock<IRepository<Comment>>();
            mockArticleRepo = new Mock<IRepository<Article>>();      
            mockArticleRepo.Setup(x => x.ReadAll()).Returns(this.FakeArticleObject);
            mockCommentRepo.Setup(r => r.ReadAll()).Returns(this.FakeCommentObject);
            articleLogic = new ArticleLogic(mockArticleRepo.Object);
            commentLogic = new CommentLogic(mockCommentRepo.Object);

        }

        [Test]
        public void UpdateTest()
        {
            Article article1 = new Article() { ArticleId = 1, Title = "Galactic Odyssey: Beyond the Stars", Category = "Sci-Fi", Likes = 20 };

            Assert.That(() => this.articleLogic.Update(article1), Throws.Nothing);

        }
        [Test]
        public void UpdateTest_WithTitleEmpty()
        {
            Article article1 = new Article() { ArticleId = 2, Title = "", Category = "Sci-Fi", Likes = 20 };

            Assert.That(() => this.articleLogic.Update(article1), Throws.TypeOf<Exception>());

        }
       
        [Test]
        public void ReadIndexOutOfRangeTest()
        {
            Assert.That(() => this.articleLogic.Read(11), Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void ReadAllTest()
        {
            Assert.That(articleLogic.ReadAll().Count, Is.EqualTo(10));
        }

        [Test]
        public void CreateMovieTestWithInCorrectTitle()
        {
            var movie = new Article() { Title = "T1" };
            try
            {
                //ACT
                articleLogic.Create(movie);
            }
            catch
            {

            }

            //ASSERT
            mockArticleRepo.Verify(r => r.Create(movie), Times.Never);
        }
        [Test]
        public void CreateMovieTestWithCorrectTitle()
        {
            var movie = new Article() { Title = "Test Title" };
            //ACT
            articleLogic.Create(movie);

            //ASSERT
            mockArticleRepo.Verify(r => r.Create(movie), Times.Once);
        }

        [Test]
        public void GetMostLikedAuthorTest()
        {
           
            var actual = articleLogic.GetMostLikedAuthor();
            Assert.That(actual.Name, Is.EqualTo("John Doe"));
        }

        [Test]
        public void AuthorStatisticsTest()
        {
            var actual = articleLogic.AuthorStatistics().ToList();
            var expected = new List<AuthorInfo>()
            {
                new AuthorInfo
                {
                    articles = 3,
                    likes = 189,
                    name = "John Doe"
                },
                new AuthorInfo
                {
                    articles = 2,
                    likes = 164,
                    name = "Emily Davis"
                },
                new AuthorInfo
                {
                    articles = 1,
                    likes = 69,
                    name = "Jane Smith"
                },
                new AuthorInfo
                {
                    articles = 2,
                    likes = 69,
                    name = "Michael Johnson"
                },
                new AuthorInfo
                {
                    articles = 2,
                    likes = 39,
                    name = "Robert White"
                },
            };

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Top3MostCommentedArticleTest()
        {
            var actual = articleLogic.Top3MostCommentedArticle().ToList();
            Assert.That(actual[0], Is.EqualTo("Pirate's Cove: Tales of the High Seas"));
            Assert.That(actual[1], Is.EqualTo("Lost Treasures of the Amazon"));
            Assert.That(actual[2], Is.EqualTo("Galactic Odyssey: Beyond the Stars"));
        }
        [Test]
        public void AvgLikesPerCategoryTest()
        {
            var actual = articleLogic.AvgLikesPerCategory().ToList();
            var expected = new List<AvgCtgLikes>()
            {
                new AvgCtgLikes
                {
                    category = "Sci-Fi",
                    AvgLikes = 41.5

                },
                new AvgCtgLikes
                {
                    category = "Fantasy",
                    AvgLikes = 53.666666666666664

                },
                new AvgCtgLikes
                {
                    category = "Adventure",
                    AvgLikes = 61.5

                },
                new AvgCtgLikes
                {
                    category = "Mistery",
                    AvgLikes = 75.5

                },
                new AvgCtgLikes
                {
                    category = "Historical",
                    AvgLikes = 12

                },
            };
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetCommentsForArticleTest()
        {
            var actual = commentLogic.GetCommentsForArticle(7).ToList();
            Assert.That(actual[0], Is.EqualTo("This is pure SH!T."));
            Assert.That(actual[1], Is.EqualTo("Good words used here!"));
            Assert.That(actual[2], Is.EqualTo("Excellent content, I enjoyed it!"));
            Assert.That(actual[3], Is.EqualTo("I have mixed feelings about this."));
        }

        private IQueryable<Comment> FakeCommentObject()
        {
            Article article1 = new Article() { ArticleId = 1, Title = "Galactic Odyssey: Beyond the Stars", Category = "Sci-Fi", Likes = 14 };
            Article article2 = new Article() { ArticleId = 2, Title = "Cybernetic Chronicles: Rise of the Machines", Category = "Sci-Fi", Likes = 69 };
            Article article3 = new Article() { ArticleId = 3, Title = "Realm of the Mystic Dragons", Category = "Fantasy", Likes = 46 };
            Article article4 = new Article() { ArticleId = 4, Title = "Enchanted Kingdoms: The Elven Legacy", Category = "Fantasy", Likes = 88 };
            Article article5 = new Article() { ArticleId = 5, Title = "Sorcerer's Apprentice: Magical Realms", Category = "Fantasy", Likes = 27 };
            Article article6 = new Article() { ArticleId = 6, Title = "Lost Treasures of the Amazon", Category = "Adventure", Likes = 23 };
            Article article7 = new Article() { ArticleId = 7, Title = "Pirate's Cove: Tales of the High Seas", Category = "Adventure", Likes = 100 };
            Article article8 = new Article() { ArticleId = 8, Title = "Whispers in the Moonlit Mansion", Category = "Mistery", Likes = 76 };
            Article article9 = new Article() { ArticleId = 9, Title = "Cryptic Conundrums: Detective Chronicles", Category = "Mistery", Likes = 75 };
            Article article10 = new Article() { ArticleId = 10, Title = "Epic Tales from Ancient Empires", Category = "Historical", Likes = 12 };


            article1.Comments = new List<Comment>();
            article2.Comments = new List<Comment>();
            article3.Comments = new List<Comment>();
            article4.Comments = new List<Comment>();
            article5.Comments = new List<Comment>();
            article6.Comments = new List<Comment>();
            article7.Comments = new List<Comment>();
            article8.Comments = new List<Comment>();
            article9.Comments = new List<Comment>();
            article10.Comments = new List<Comment>();


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


            c0.Article = article1;
            c1.Article = article2;
            c2.Article = article3;
            c3.Article = article4;
            c4.Article = article5;

            c5.Article = article6;
            c6.Article = article6;
            c7.Article = article6;

            c8.Article = article7;
            c9.Article = article7;
            c10.Article = article7;
            c11.Article = article7
                ;
            c12.Article = article8;
            c13.Article = article9;
            c14.Article = article10;


            c0.ArticleId = article1.ArticleId; article1.Comments.Add(c0);
            c1.ArticleId = article2.ArticleId; article2.Comments.Add(c1);
            c2.ArticleId = article3.ArticleId; article3.Comments.Add(c2);
            c3.ArticleId = article4.ArticleId; article4.Comments.Add(c3);
            c4.ArticleId = article5.ArticleId; article5.Comments.Add(c4);

            c5.ArticleId = article6.ArticleId; article6.Comments.Add(c5);
            c6.ArticleId = article6.ArticleId; article6.Comments.Add(c6);
            c7.ArticleId = article6.ArticleId; article6.Comments.Add(c7);

            c8.ArticleId = article7.ArticleId; article7.Comments.Add(c8);
            c9.ArticleId = article7.ArticleId; article7.Comments.Add(c9);
            c10.ArticleId = article7.ArticleId; article7.Comments.Add(c10);
            c11.ArticleId = article7.ArticleId; article7.Comments.Add(c11);

            c12.ArticleId = article8.ArticleId; article8.Comments.Add(c12);
            c13.ArticleId = article9.ArticleId; article9.Comments.Add(c13);
            c14.ArticleId = article10.ArticleId; article10.Comments.Add(c14);


            List<Comment> items = new List<Comment>();
            items.Add(c0);
            items.Add(c1);
            items.Add(c2);
            items.Add(c3);
            items.Add(c4);
            items.Add(c5);
            items.Add(c6);
            items.Add(c7);
            items.Add(c8);
            items.Add(c9);
            items.Add(c10);
            items.Add(c11);
            items.Add(c12);
            items.Add(c13);
            items.Add(c14);

            return items.AsQueryable();

        }
        private IQueryable<Article> FakeArticleObject()
        {
            Article article1 = new Article() { ArticleId = 1, Title = "Galactic Odyssey: Beyond the Stars", Category = "Sci-Fi", Likes = 14 };
            Article article2 = new Article() { ArticleId = 2, Title = "Cybernetic Chronicles: Rise of the Machines", Category = "Sci-Fi", Likes = 69 };
            Article article3 = new Article() { ArticleId = 3, Title = "Realm of the Mystic Dragons", Category = "Fantasy", Likes = 46 };
            Article article4 = new Article() { ArticleId = 4, Title = "Enchanted Kingdoms: The Elven Legacy", Category = "Fantasy", Likes = 88 };
            Article article5 = new Article() { ArticleId = 5, Title = "Sorcerer's Apprentice: Magical Realms", Category = "Fantasy", Likes = 27 };
            Article article6 = new Article() { ArticleId = 6, Title = "Lost Treasures of the Amazon", Category = "Adventure", Likes = 23 };
            Article article7 = new Article() { ArticleId = 7, Title = "Pirate's Cove: Tales of the High Seas", Category = "Adventure", Likes = 100 };
            Article article8 = new Article() { ArticleId = 8, Title = "Whispers in the Moonlit Mansion", Category = "Mistery", Likes = 76 };
            Article article9 = new Article() { ArticleId = 9, Title = "Cryptic Conundrums: Detective Chronicles", Category = "Mistery", Likes = 75 };
            Article article10 = new Article() { ArticleId = 10, Title = "Epic Tales from Ancient Empires", Category = "Historical", Likes = 12 };


            article1.Comments = new List<Comment>();
            article2.Comments = new List<Comment>();
            article3.Comments = new List<Comment>();
            article4.Comments = new List<Comment>();
            article5.Comments = new List<Comment>();
            article6.Comments = new List<Comment>();
            article7.Comments = new List<Comment>();
            article8.Comments = new List<Comment>();
            article9.Comments = new List<Comment>();
            article10.Comments = new List<Comment>();

            

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


            Author a1 = new Author() { AuthorId = 1, Name = "John Doe" };
            Author a2 = new Author() { AuthorId = 2, Name = "Jane Smith" };
            Author a3 = new Author() { AuthorId = 3, Name = "Michael Johnson" };
            Author a4 = new Author() { AuthorId = 4, Name = "Emily Davis" };
            Author a5 = new Author() { AuthorId = 5, Name = "Robert White" };

            a1.Articles = new List<Article>();
            a2.Articles = new List<Article>();
            a3.Articles = new List<Article>();
            a4.Articles = new List<Article>();
            a5.Articles = new List<Article>();


            article1.Author = a1;
            article2.Author = a2;
            article3.Author = a3;
            article4.Author = a4;
            article5.Author = a5;
            article6.Author = a3;
            article7.Author = a1;
            article8.Author = a4;
            article9.Author = a1;
            article10.Author = a5;


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


            c0.Article = article1;
            c1.Article = article2;
            c2.Article = article3;
            c3.Article = article4;
            c4.Article = article5;

            c5.Article = article6;
            c6.Article = article6;
            c7.Article = article6;

            c8.Article = article7;
            c9.Article = article7;
            c10.Article = article7;
            c11.Article = article7;
                
            c12.Article = article8;
            c13.Article = article9;
            c14.Article = article10;


            c0.ArticleId = article1.ArticleId; article1.Comments.Add(c0);
            c1.ArticleId = article2.ArticleId; article2.Comments.Add(c1);
            c2.ArticleId = article3.ArticleId; article3.Comments.Add(c2);
            c3.ArticleId = article4.ArticleId; article4.Comments.Add(c3);
            c4.ArticleId = article5.ArticleId; article5.Comments.Add(c4);

            c5.ArticleId = article6.ArticleId; article6.Comments.Add(c5);
            c6.ArticleId = article6.ArticleId; article6.Comments.Add(c6);
            c7.ArticleId = article6.ArticleId; article6.Comments.Add(c7);

            c8.ArticleId = article7.ArticleId; article7.Comments.Add(c8);
            c9.ArticleId = article7.ArticleId; article7.Comments.Add(c9);
            c10.ArticleId = article7.ArticleId; article7.Comments.Add(c10);
            c11.ArticleId = article7.ArticleId; article7.Comments.Add(c11);

            c12.ArticleId = article8.ArticleId; article8.Comments.Add(c12);
            c13.ArticleId = article9.ArticleId; article9.Comments.Add(c13);
            c14.ArticleId = article10.ArticleId; article10.Comments.Add(c14);


            List<Article> items = new List<Article>();
            items.Add(article1);
            items.Add(article2);
            items.Add(article3);
            items.Add(article4);
            items.Add(article5);
            items.Add(article6);
            items.Add(article7);
            items.Add(article8);
            items.Add(article9);
            items.Add(article10);


            return items.AsQueryable();
            

        }
        
    }
}
