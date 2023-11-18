using NIA6HM_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIA6HM_HFT_2023241.Repository
{
    public class CommentRepository : Repository<Comment>, IRepository<Comment>
    {
        public CommentRepository(BlogDbContext ctx) : base(ctx)
        {
        }

        public override Comment Read(int id)
        {
            return ctx.Comments.FirstOrDefault(t => t.CommentId == id);
        }

        public override void Update(Comment item)
        {
            var old = Read(item.CommentId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
