using System;
using System.Linq;
using NIA6HM_HFT_2023241.Models;
using ConsoleTools;
using System.Numerics;
using System.Collections.Generic;

namespace NIA6HM_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            if (entity == "Author")
            {
                Console.Write("Enter Author Name: ");
                string name = Console.ReadLine();
                rest.Post(new Author() { Name = name }, "author");
            }
            else if (entity == "Article")
            {
                Console.Write("Enter Article Title: ");
                string name = Console.ReadLine();
                rest.Post(new Article() { Title = name }, "article");
            }
            else if(entity == "Comment")
            {
                Console.Write("Enter Comment Text: ");
                string name = Console.ReadLine();
                rest.Post(new Comment() { Text = name }, "comment");
            }
        }
        static void List(string entity)
        {
            if (entity == "Author")
            {
                List<Author> authors = rest.Get<Author>("author");
                foreach (var item in authors)
                {
                    Console.WriteLine(item.AuthorId + ": " + item.Name);
                }
            }
            else if(entity == "Article")
            {
                List<Article> actors = rest.Get<Article>("article");
                foreach (var item in actors)
                {
                    Console.WriteLine(item.ArticleId + ": " + item.Title);
                }
            }
            else if(entity == "Comment")
            {
                List<Comment> actors = rest.Get<Comment>("comment");
                foreach (var item in actors)
                {
                    Console.WriteLine(item.CommentId + ": " + item.Text);
                }               
            }
            Console.ReadLine();

        }
        static void Update(string entity)
        {
            if (entity == "Author")
            {
                Console.Write("Enter Author's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Author one = rest.Get<Author>(id, "author");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "author");
            }
            else if (entity == "Article")
            {
                Console.Write("Enter Article's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Article one = rest.Get<Article>(id, "article");
                Console.Write($"New title [old: {one.Title}]: ");
                string name = Console.ReadLine();
                one.Title = name;
                rest.Put(one, "article");
            }
            else if (entity == "Comment")
            {
                Console.Write("Enter Comment's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Comment one = rest.Get<Comment>(id, "comment");
                Console.Write($"New comment [old: {one.Text}]: ");
                string name = Console.ReadLine();
                one.Text = name;
                rest.Put(one, "comment");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Author")
            {
                Console.Write("Enter Author's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "author");
            }
            else if (entity == "Article")
            {
                Console.Write("Enter Article's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "article");
            }
            else if (entity == "comment")
            {
                Console.Write("Enter Comment's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "comment");
            }
        }
        static void AuthorStatistics(string entity)
        {
            if (entity == "AuthorStatistics")
            {
                var q = rest.Get<AuthorInfo>("stat/AuthorStatistics").ToList();
                foreach (var item in q)
                {
                    Console.WriteLine("Name: " + item.name + " " + "articles: " + item.articles + " " + "Likes: " + item.likes);
                }
                Console.ReadLine();
            }
                
        }
        static void GetMostLikedAuthor(string entity)
        {
            if (entity == "GetMostLikedAuthor")
            {
                var q = rest.Get<AuthorInfo>("stat/GetMostLikedAuthor");
                foreach (var item in q)
                {
                    Console.WriteLine("Name: " + item.name);
                }

              
                Console.ReadLine();
            }

        }
        static void Top3MostCommentedArticle(string entity)
        {
            if (entity == "Top3MostCommentedArticle")
            {
                var q = rest.Get<dynamic>("stat/Top3MostCommentedArticle");
                foreach (var item in q)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.ReadLine();
            }
        }
        static void AvgLikesPerCategory(string entity)
        {
            if (entity == "AvgLikesPerCategory")
            {
                var q = rest.Get<AvgCtgLikes>("stat/AvgLikesPerCategory").ToList();
                foreach (var item in q)
                {
                    Console.WriteLine("Category: " + item.category + " " + "Average like: " + item.AvgLikes);
                }
                Console.ReadLine();
            }
        }

        static void GetCommentsForArticle(string entity)
        {
            if (entity == "GetCommentsForArticle")
            {
                Console.WriteLine("Enter article's ID: ");
                int artID = int.Parse(Console.ReadLine());
                var q = rest.Get<dynamic>($"stat/GetCommentsForArticle/{artID}");

                foreach (var item in q)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.ReadLine();
            }
                
        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:58339/", "swagger");

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

            var noncrudSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Authors Statistics", () => AuthorStatistics("AuthorStatistics"))
                .Add("AvgLikesPerCategory", () => AvgLikesPerCategory("AvgLikesPerCategory"))
                .Add("GetMostLikedAuthor", () => GetMostLikedAuthor("GetMostLikedAuthor"))
                .Add("Top3MostCommentedArticle", () => Top3MostCommentedArticle("Top3MostCommentedArticle"))
                .Add("GetCommentsForArticle", () => GetCommentsForArticle("GetCommentsForArticle"))
                .Add("Exit", ConsoleMenu.Close);
                
            

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Author", () => authorSubMenu.Show())
                .Add("Article", () => articleSubMenu.Show())
                .Add("Comment", () => commentSubMenu.Show())
                .Add("Queries", () => noncrudSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}
