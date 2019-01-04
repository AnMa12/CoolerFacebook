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
        public FriendRequest() { }
        public FriendRequest(Profile sender, Profile receiver)
        {
            this.Sender = sender;
            this.Receiver = receiver;
        }
    }
}