using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolerFacebook.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string Name { get; set;  }
        public virtual ICollection<Mesaj> Mesajs { get; set; }
    }
}