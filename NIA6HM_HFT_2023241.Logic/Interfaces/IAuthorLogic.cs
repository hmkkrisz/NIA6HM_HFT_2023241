using System.Linq;
using NIA6HM_HFT_2023241.Models;

namespace NIA6HM_HFT_2023241.Logic
{
    public interface IAuthorLogic
    {
        void Create(Author item);
        void Delete(int id);
        Author Read(int id);
        IQueryable<Author> ReadAll();
        void Update(Author item);
    }
}
