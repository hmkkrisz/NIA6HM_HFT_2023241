using NIA6HM_HFT_2023241.Models;
using NIA6HM_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIA6HM_HFT_2023241.Logic
{
    public class AuthorLogic : IAuthorLogic
    {
        IRepository<Author> repo;
        public AuthorLogic(IRepository<Author> repo)
        {
            this.repo = repo;
        }
        public void Create(Author item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Author Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Author> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Author item)
        {
            this.repo.Update(item);
        }
    }
}
