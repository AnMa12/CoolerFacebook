using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolerFacebook.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        [Required]
        public string Path { get; set; }
        public string Description { get; set; }

        public virtual Album Album { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}