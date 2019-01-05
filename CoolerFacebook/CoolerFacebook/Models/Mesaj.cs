using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolerFacebook.Models
{
    public class Mesaj
    {
        [Key]
        public int MesajId { get; set; }
        public string text { get; set; }
        public virtual Group Group { get; set; }
        public virtual Profile Profile { get; set; }

        public Mesaj() { }
        public Mesaj(string text, Group group, Profile profile)
        {
            this.text = text;
            this.Group = group;
            this.Profile = profile;
        }
    }
}