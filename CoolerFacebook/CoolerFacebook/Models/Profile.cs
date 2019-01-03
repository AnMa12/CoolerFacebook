using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolerFacebook.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public bool Visibility { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        //colectie albume
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}