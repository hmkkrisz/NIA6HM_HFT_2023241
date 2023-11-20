﻿using Microsoft.EntityFrameworkCore;
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
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Article Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Article> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Article item)
        {
            this.repo.Update(item);
        }

        //NON-CRUD------

        public IEnumerable<LikeInfo> AuthorLikeStatistics()
        {
            var likes = (from x in this.repo.ReadAll()
                        group x by x.Author.Name into g
                        select new LikeInfo()
                        {
                            name = g.Key,
                            likes = g.Sum(t => t.Likes)

                        }).OrderByDescending(x =>x.likes);

            return likes;
        }
        public IEnumerable<Article> MostComments()
        {

            var comments = this.repo.ReadAll()
                .OrderByDescending(x => x.Comments.Count());
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
        public IEnumerable<Comment> GetCommentsForArticle(int articleId)
        {
            var comments = repo.ReadAll()
                .Where(x => x.ArticleId == articleId)
                .SelectMany(x => x.Comments).ToList();

            return comments;
        }

        public class LikeInfo
        {
            public string name { get; set; }
            public int? likes { get; set; }
            
        }

        public class AvgCtgLikes
        {
            public string category { get; set;}
            public double? AvgLikes { get; set; }
        }

    }
}
