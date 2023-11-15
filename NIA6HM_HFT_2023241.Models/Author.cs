using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NIA6HM_HFT_2023241.Models
{
    public class Author
    {       
        [Key] public int AuthorId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
