using NIA6HM_HFT_2023241.Models;
using NIA6HM_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIA6HM_HFT_2023241.Logic
{
    public class CommentLogic : ICommentLogic
    {
        IRepository<Comment> repo;
        public CommentLogic(IRepository<Comment> repo)
        {
            this.repo = repo;
        }
        public void Create(Comment item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Comment Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Comment> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Comment item)
        {
            this.repo.Update(item);
        }
    }
}
