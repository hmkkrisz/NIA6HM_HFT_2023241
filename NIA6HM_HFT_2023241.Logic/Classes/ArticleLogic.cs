using NIA6HM_HFT_2023241.Models;
using NIA6HM_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
