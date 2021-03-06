﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolerFacebook.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public Comment() { }
        public Comment(string text, Photo pic , ApplicationUser user )
        {
            this.Text = text;
            this.Date = DateTime.Now;
            this.Photo = pic;
            this.Author = user;

        }
    }
}