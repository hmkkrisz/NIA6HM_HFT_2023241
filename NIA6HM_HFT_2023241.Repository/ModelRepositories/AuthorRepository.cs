using NIA6HM_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIA6HM_HFT_2023241.Repository
{
    public class AuthorRepository : Repository<Author>, IRepository<Author>
    {
        public AuthorRepository(BlogDbContext ctx) : base(ctx)
        {
        }

        public override Author Read(int id)
        {
            return ctx.Authors.FirstOrDefault(t => t.AuthorId == id);
        }

        public override void Update(Author item)
        {
            var old = Read(item.AuthorId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
