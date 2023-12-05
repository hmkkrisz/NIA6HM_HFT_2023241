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

        static void CreateAuthor(string entity)
        {
            if (entity == "Author")
            {
                Console.Write("Enter Author Name: ");
                string name = Console.ReadLine();
                rest.Post(new Author() { Name = name }, "author");
            }
        }
        static void CreateArticle(string entity)
        {
            if (entity == "Article")
            {
                Console.Write("Enter Article Title: ");
                string name = Console.ReadLine();
                rest.Post(new Article() { Title = name }, "article");
            }
        }
        static void CreateComment(string entity)
        {
            if (entity == "Comment")
            {
                Console.Write("Enter Comment Text: ");
                string name = Console.ReadLine();
                rest.Post(new Comment() { Text = name }, "comment");
            }
        }
        static void ListAuthor(string entity)
        {
            if (entity == "Author")
            {
                List<Author> authors = rest.Get<Author>("author");
                foreach (var item in authors)
                {
                    Console.WriteLine(item.AuthorId + ": " + item.Name);
                }
            }
            Console.ReadLine();
        }
        static void ListArticle(string entity)
        {
            if (entity == "Article")
            {
                List<Article> actors = rest.Get<Article>("article");
                foreach (var item in actors)
                {
                    Console.WriteLine(item.ArticleId + ": " + item.Title);
                }
            }
            Console.ReadLine();
        }
        static void ListComment(string entity)
        {
            if (entity == "Author")
            {
                List<Comment> actors = rest.Get<Comment>("comment");
                foreach (var item in actors)
                {
                    Console.WriteLine(item.CommentId + ": " + item.Text);
                }
            }
            Console.ReadLine();
        }
        static void UpdateAuthor(string entity)
        {
            if (entity == "Author")
            {
                Console.Write("Enter Author's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Author one = rest.Get<Author>(id, "actor");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "actor");
            }
        }
        static void UpdateArticle(string entity)
        {
            if (entity == "Article")
            {
                Console.Write("Enter Article's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Article one = rest.Get<Article>(id, "article");
                Console.Write($"New title [old: {one.Title}]: ");
                string name = Console.ReadLine();
                one.Title = name;
                rest.Put(one, "article");
            }
        }
        static void UpdateComment(string entity)
        {
            if (entity == "Comment")
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
        static void DeleteAuthor(string entity)
        {
            if (entity == "Author")
            {
                Console.Write("Enter Author's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "author");
            }
        }
        static void DeleteArticle(string entity)
        {
            if (entity == "Article")
            {
                Console.Write("Enter Article's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "article");
            }
        }
        static void DeleteComment(string entity)
        {
            if (entity == "comment")
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
            if (entity == "Query")
            {
                var q = rest.Get<dynamic>("stat/GetMostLikedAuthor");
                foreach (var item in q)
                {
                    Console.WriteLine("Name: " + item.ToString());
                }
                Console.ReadLine();
            }

        }
        static void Top3MostCommentedArticle(string entity)
        {
            if (entity == "Query")
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
            if (entity == "Query")
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
            if (entity == "Query")
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
                .Add("List", () => ListAuthor("Author"))
                .Add("Create", () => CreateAuthor("Author"))
                .Add("Delete", () => DeleteAuthor("Author"))
                .Add("Update", () => UpdateAuthor("Author"))
                .Add("Exit", ConsoleMenu.Close);

            var articleSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => ListArticle("Article"))
                .Add("Create", () => CreateArticle("Article"))
                .Add("Delete", () => DeleteArticle("Article"))
                .Add("Update", () => UpdateArticle("Article"))
                .Add("Exit", ConsoleMenu.Close);

            var commentSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => ListComment("Comment"))
                .Add("Create", () => CreateComment("Comment"))
                .Add("Delete", () => DeleteComment("Comment"))
                .Add("Update", () => UpdateComment("Comment"))
                .Add("Exit", ConsoleMenu.Close);

            var noncrudSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Authors Statistics", () => AuthorStatistics("AuthorStatistics"))
                .Add("AvgLikesPerCategory", () => AvgLikesPerCategory("Query"))
                .Add("GetMostLikedAuthor", () => GetMostLikedAuthor("Query"))
                .Add("Top3MostCommentedArticle", () => Top3MostCommentedArticle("Query"))
                .Add("GetCommentsForArticle", () => GetCommentsForArticle("Query"))
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
