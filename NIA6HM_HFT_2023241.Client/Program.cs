using System;
using System.Linq;
using NIA6HM_HFT_2023241.Models;
using NIA6HM_HFT_2023241.Repository;
using NIA6HM_HFT_2023241.Logic;


namespace NIA6HM_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            var ctx = new BlogDbContext();
            var ArticleRepo = new ArticleRepository(ctx);
            var ArticleLogic = new ArticleLogic(ArticleRepo);

            var CommentRepo = new CommentRepository(ctx);
            var CommentLogic = new CommentLogic(CommentRepo);

            var articleNC0 = ArticleLogic.AuthorStatistics();

            var articleNC1 = ArticleLogic.AvgLikesPerCategory();
            
            var articleNC2 = ArticleLogic.Top3MostCommentedArticle();

            var articleNC3 = ArticleLogic.GetMostLikedAuthor();

            var commentNC = CommentLogic.GetCommentsForArticle(7);





            ;
        }
    }
}
