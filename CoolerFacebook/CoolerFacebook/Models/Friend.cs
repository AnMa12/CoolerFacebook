using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolerFacebook.Models
{
    public class Friend
    {
        [Key]
        public int FriendId { get; set; }
        public virtual Profile Friend1 { get; set; }
        public virtual Profile Friend2 { get; set; }

    }
}