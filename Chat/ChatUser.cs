using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeClip.LiveChat.Chat
{
    public class ChatUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ConnectionId { get; set; }
        public bool IsAvailable
        {
            get
            {
                return !string.IsNullOrEmpty(ConnectionId);
            }
        }

        public void Reset()
        {
            Name = string.Empty;
            Email = string.Empty;
            ConnectionId = string.Empty;
        }
    }
}