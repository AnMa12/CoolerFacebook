using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolerFacebook.Models
{
    public class ProfileGroup
    {
        [Key]
        public int GroupId { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Group Group { get; set; }

        public ProfileGroup() { }
        public ProfileGroup(Profile profile, Group group)
        {
            this.Group = group;
            this.Profile = profile;
        }
    }
}