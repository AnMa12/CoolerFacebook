using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolerFacebook.Models
{
    public class FriendRequest
    {
        [Key]
        public int RequestId { get; set; }
        public virtual Profile Sender { get; set; }
        public virtual Profile Receiver { get; set; }
    }
}