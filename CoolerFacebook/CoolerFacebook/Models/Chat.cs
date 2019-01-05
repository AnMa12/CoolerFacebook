using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolerFacebook.Models
{
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }
        public virtual Profile Friend1 { get; set; }
        public virtual Profile Friend2 { get; set; }

        public virtual ICollection<Message> Messages { get; set;}

        public Chat() { }
        public Chat(Profile fr1, Profile fr2)
        {
            this.Friend1 = fr1;
            this.Friend2 = fr2;
        }
    }
}