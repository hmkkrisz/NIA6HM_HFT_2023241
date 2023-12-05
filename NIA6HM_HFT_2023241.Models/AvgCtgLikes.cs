using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIA6HM_HFT_2023241.Models
{
    public class AvgCtgLikes
    {
        public string category { get; set; }
        public double? AvgLikes { get; set; }

        public override bool Equals(object obj)
        {
            AvgCtgLikes b = obj as AvgCtgLikes;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.category == b.category
                    && this.AvgLikes == b.AvgLikes;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.category, this.AvgLikes);
        }
    }
}
