using System;
using System.Linq;
using NIA6HM_HFT_2023241.Models;
using NIA6HM_HFT_2023241.Repository;
using NIA6HM_HFT_2023241.Logic;
using ConsoleTools;

namespace NIA6HM_HFT_2023241.Client
{
    internal class Program
    {
        static AuthorLogic authorLogic;
        static ArticleLogic articleLogic;
        static CommentLogic commentLogic;
        static void Create(string entity)
        {
            Console.WriteLine(entity + " create");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "Author")
            {
                var items = authorLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name");
                foreach (var item in items)
                {
                    Console.WriteLine(item.AuthorId + "\t" + item.Name);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine(entity + " update");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.WriteLine(entity + " delete");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
          
            var ctx = new BlogDbContext();

            var CommentRepo = new CommentRepository(ctx);
            var ArticleRepo = new ArticleRepository(ctx);
            var AuthorRepo = new AuthorRepository(ctx);

            articleLogic = new ArticleLogic(ArticleRepo);
            authorLogic = new AuthorLogic(AuthorRepo);           
            commentLogic = new CommentLogic(CommentRepo);


            var authorSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Author"))
                .Add("Create", () => Create("Author"))
                .Add("Delete", () => Delete("Author"))
                .Add("Update", () => Update("Author"))
                .Add("Exit", ConsoleMenu.Close);

            var articleSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Article"))
                .Add("Create", () => Create("Article"))
                .Add("Delete", () => Delete("Article"))
                .Add("Update", () => Update("Article"))
                .Add("Exit", ConsoleMenu.Close);

            var commentSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Comment"))
                .Add("Create", () => Create("Comment"))
                .Add("Delete", () => Delete("Comment"))
                .Add("Update", () => Update("Comment"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Author", () => authorSubMenu.Show())
                .Add("Article", () => articleSubMenu.Show())
                .Add("Comment", () => commentSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();


            //var articleNC0 = ArticleLogic.AuthorStatistics();

            //var articleNC1 = ArticleLogic.AvgLikesPerCategory();

            //var articleNC2 = ArticleLogic.Top3MostCommentedArticle();

            //var articleNC3 = ArticleLogic.GetMostLikedAuthor();

            //var commentNC = CommentLogic.GetCommentsForArticle(7);





            ;
        }
    }
}
