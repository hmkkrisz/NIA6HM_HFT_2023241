using NIA6HM_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIA6HM_HFT_2023241.Repository
{
    public class ArticleRepository : Repository<Article>, IRepository<Article>
    {
        public ArticleRepository(BlogDbContext ctx) : base(ctx)
        {
        }

        public override Article Read(int id)
        {
            return ReadAll().FirstOrDefault(t => t.ArticleId == id);
        }

        public override void Update(Article item)
        {
            var old = Read(item.ArticleId);
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
