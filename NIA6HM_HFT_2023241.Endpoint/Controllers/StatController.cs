using Microsoft.AspNetCore.Mvc;
using NIA6HM_HFT_2023241.Logic;
using NIA6HM_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;
using static NIA6HM_HFT_2023241.Logic.ArticleLogic;

namespace NIA6HM_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IArticleLogic articleLogic;
        ICommentLogic commentLogic;

        public StatController(IArticleLogic articleLogic, ICommentLogic commentLogic)
        {
            this.articleLogic = articleLogic;
            this.commentLogic = commentLogic;
        }

        [HttpGet]
        public IEnumerable<AuthorInfo> AuthorStatistics()
        {
            return this.articleLogic.AuthorStatistics();

        }
        [HttpGet]
        public IQueryable<string> Top3MostCommentedArticle()
        {
            return this.articleLogic.Top3MostCommentedArticle();
        }
        [HttpGet]
        public IEnumerable<AvgCtgLikes> AvgLikesPerCategory()
        {
            return this.articleLogic.AvgLikesPerCategory();
        }
        [HttpGet]
        public IEnumerable<AuthorInfo> GetMostLikedAuthor()
        {
            return this.articleLogic.GetMostLikedAuthor();
        }
        [HttpGet("{id}")]
        public IQueryable<string> GetCommentsForArticle(int id)
        {
            return this.commentLogic.GetCommentsForArticle(id);
        }
    }
}