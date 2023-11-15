using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIA6HM_HFT_2023241.Models
{
    public class Comment
    {
        [Key] public int CommentId { get; set; }
        public string Text { get; set; }

        [ForeignKey(nameof(Article))]public int ArticleId { get; set; }
        
        public virtual Article Article { get; set; }
    }
}
