using System;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public string MessageBody { get; set; }

        public DateTime MessageDtTm { get; set; }

        public virtual ChatUser FromUser { get; set; }

    }
}
