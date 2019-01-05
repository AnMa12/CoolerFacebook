using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolerFacebook.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public virtual Chat Chat { get; set; }
        public virtual Profile Sender { get; set; }

        public Message() { }
        public Message(string text, Chat chat, Profile sender)
        {
            this.Text = text;
            this.Date = DateTime.Now;
            this.Chat = chat;
            this.Sender = sender;
        }
    }
}