using System.Linq;
using NIA6HM_HFT_2023241.Models;

namespace NIA6HM_HFT_2023241.Logic
{
    public interface ICommentLogic
    {
        void Create(Comment item);
        void Delete(int id);
        Comment Read(int id);
        IQueryable<Comment> ReadAll();
        void Update(Comment item);
    }
}
