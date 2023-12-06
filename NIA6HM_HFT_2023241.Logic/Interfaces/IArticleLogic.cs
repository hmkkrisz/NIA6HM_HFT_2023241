using System.Collections.Generic;
using System.Linq;
using NIA6HM_HFT_2023241.Models;
using static NIA6HM_HFT_2023241.Logic.ArticleLogic;

namespace NIA6HM_HFT_2023241.Logic
{
    public interface IArticleLogic
    {
        void Create(Article item);
        void Delete(int id);
        Article Read(int id);
        IQueryable<Article> ReadAll();
        void Update(Article item);
        IEnumerable<AuthorInfo> AuthorStatistics();
        IQueryable<string> Top3MostCommentedArticle();
        IEnumerable<AvgCtgLikes> AvgLikesPerCategory();
        IEnumerable<AuthorInfo> GetMostLikedAuthor();


    }
}
