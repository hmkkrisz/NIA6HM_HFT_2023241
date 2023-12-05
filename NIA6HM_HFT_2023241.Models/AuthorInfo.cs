using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIA6HM_HFT_2023241.Models
{
    public class AuthorInfo
    {
        public string name { get; set; }
        public int? likes { get; set; }
        public int articles { get; set; }


        public override bool Equals(object obj)
        {
            AuthorInfo b = obj as AuthorInfo;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.name == b.name
                    && this.likes == b.likes
                    && this.articles == b.articles;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.name, this.likes, this.articles);
        }

    }
}
