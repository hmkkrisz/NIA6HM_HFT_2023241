﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIA6HM_HFT_2023241.Models
{
    public class Article
    {
        [Key]public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set;}

        [ForeignKey(nameof(Author))] public int AuthorId { get; set; }
    }

}