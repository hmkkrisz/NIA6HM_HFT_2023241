using Castle.DynamicProxy.Generators.Emitters;
using Microsoft.EntityFrameworkCore;
using NIA6HM_HFT_2023241.Models;
using NIA6HM_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NIA6HM_HFT_2023241.Logic
{
    public class ArticleLogic : IArticleLogic
    {
        IRepository<Article> repo;
        public ArticleLogic(IRepository<Article> repo)
        {
            this.repo = repo;
        }
        public void Create(Article item)
        {
            if (item.Title.Length < 3)
            {
                throw new ArgumentException("title too short...");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        { 
            this.repo.Delete(id); 
        }

        public Article Read(int id)
        {
            if (id < repo.ReadAll().Count())
                return repo.Read(id);
            else
                throw new IndexOutOfRangeException("[ERR] ID was too big!");

            //var article = this.repo.Read(id);
            //if (article == null)
            //{
            //    throw new ArgumentException("Article not exists");
            //}
            //return article;
        }

        public IQueryable<Article> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Article item)
        {
            if (item.Title == "")
            {
                throw new Exception("[ERR] Title can't be empty!");
            }


            this.repo.Update(item);
        }

        //NON-CRUD------

        public IEnumerable<AuthorInfo> AuthorStatistics()
        {
            var likes = (from x in this.repo.ReadAll()
                         group x by x.Author.Name into g
                         select new AuthorInfo()
                         {
                             name = g.Key,
                             articles = g.Count(),
                             likes = g.Sum(t => t.Likes)


                         }).OrderByDescending(x =>x.likes);

            return likes;
        }
        
        public IQueryable<string> Top3MostCommentedArticle()
        {

            var comments = this.repo.ReadAll()
                .OrderByDescending(x => x.Comments.Count()).Take(3)
                .Select(t => t.Title);
            return comments;
        }

        public IEnumerable<AvgCtgLikes> AvgLikesPerCategory()
        {
            return from x in this.repo.ReadAll()
                   group x by x.Category into g
                   select new AvgCtgLikes()
                   {
                       category = g.Key,
                       AvgLikes = g.Average(x => x.Likes)

                   };
        }
        public Author GetMostLikedAuthor()
        {
            var author = this.repo.ReadAll()
                .OrderByDescending(x => x.Likes).FirstOrDefault().Author;
            return author;
               
        }

        public class AuthorInfo
        {
            public string name { get; set; }
            public int? likes { get; set; }
            public int articles { get; set; }


            public override bool Equals(object obj)
            {
                AuthorInfo b = obj as AuthorInfo;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.name == b.name
                        && this.likes == b.likes
                        && this.articles == b.articles;
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.name, this.likes, this.articles);
            }

        }

        public class AvgCtgLikes
        {
            public string category { get; set;}
            public double? AvgLikes { get; set; }

            public override bool Equals(object obj)
            {
                AvgCtgLikes b = obj as AvgCtgLikes;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.category == b.category
                        && this.AvgLikes == b.AvgLikes;                      
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.category, this.AvgLikes);
            }
        }

    }
}
