using System;
using System.Linq;
using NIA6HM_HFT_2023241.Models;
using NIA6HM_HFT_2023241.Repository;

namespace NIA6HM_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
         
            IRepository<Article> repo = new ArticleRepository(new BlogDbContext());

            var items = repo.ReadAll().ToArray();

            ;
        }
    }
}
