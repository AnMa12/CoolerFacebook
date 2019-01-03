using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolerFacebook.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}