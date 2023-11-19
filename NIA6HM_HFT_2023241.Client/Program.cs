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

            var nc = ArticleLogic.YearStatistics();
            var nc2 = ArticleLogic.AvgLikesPerCategory();
            
            var nc1 = ArticleLogic.MostComments();
            

            ;
            
        }
    }
}
