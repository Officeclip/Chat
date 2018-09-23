using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OfficeClip.LiveChat.Chat
{
    public partial class Agent : System.Web.UI.Page
    {
        private string ActiveSessionsJson
        {
            get
            {
                var activeSessions = new List<string[]>();
                var chatSessions = (new ChatDb()).GetChatSessions(true);
                foreach (ChatSession chatSession in chatSessions)
                {
                    var strings = new string[5];
                    strings[0] = $"{chatSession.EndUserName}<br/>{chatSession.EndUserEmail}";
                    strings[1] = $"{chatSession.StartTime.ToString("d")}<br/>{chatSession.StartTime.ToString("t")}";
                    switch (chatSession.EndUserMode)
                    {
                        case EndUserMode.Waiting:
                            strings[2] = $"<button type='A' sid='{chatSession.ChatSessionId}'>Accept</button>";
                            break;
                        case EndUserMode.Connected:
                            strings[2] = $"<button type='R' sid='{chatSession.ChatSessionId}'>Release</button>";
                            break;
                    }
                    strings[2] += $"<button type='E' sid='{chatSession.ChatSessionId}'>End</button>";
                    activeSessions.Add(strings);
                }
                var json = JsonConvert.SerializeObject(activeSessions);
                return json;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}