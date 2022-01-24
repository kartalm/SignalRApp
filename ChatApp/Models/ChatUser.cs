using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ChatApp.Models
{
    public class ChatUser : IdentityUser
    {
        public virtual ICollection<Message> Messages { get; set; }

    }
}