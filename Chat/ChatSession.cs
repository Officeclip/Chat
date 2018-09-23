using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeClip.LiveChat.Chat
{
    public class ChatSession
    {
        public int ChatSessionId { get; set; }
        public string EndUserName { get; set; }
        public string EndUserEmail { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public EndUserMode EndUserMode { get; set; }
    }
}