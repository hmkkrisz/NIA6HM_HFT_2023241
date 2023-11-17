using System;
using NIA6HM_HFT_2023241.Repository;

namespace NIA6HM_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            BlogDbContext ctx = new BlogDbContext();

            foreach(var item in ctx.Articles)
            {
                Console.WriteLine(item.Author.Name + ":" + item.Title);
            }
            ;
        }
    }
}
