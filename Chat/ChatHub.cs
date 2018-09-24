﻿using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace OfficeClip.LiveChat.Chat
{
    public class ChatHub : Hub
    {
        protected static ChatUser agent = new ChatUser { IsEndUser = false };
        protected static ChatUser customer = new ChatUser { IsEndUser = true };
        protected static bool isConnected = false;
        protected static int AgentUserId = 1;

        private dynamic AgentConnection
        {
            get
            {
                return Clients.Client(agent.ConnectionId);
            }
        }

        private dynamic CustomerConnection
        {
            get
            {
                return Clients.Client(customer.ConnectionId);
            }
        }

        public void PopulateAgent()
        {
            List<ChatUser> users = (new ChatDb()).GetUsers();
            var user = users.Where(s => s.Id == AgentUserId).FirstOrDefault();
            (new ChatDb()).UpdateUserConnection(user.Id, Context.ConnectionId);
            PopulateAgent(user.Name, user.Email);
        }

        public void PopulateAgent(string name, string email)
        {
            agent.Name = name;
            agent.Email = email;
            agent.ConnectionId = Context.ConnectionId;

            //AgentConnection.showWaitingForChat();
            AgentConnection.showAvailableAgent(ActiveSessionsJson);
        }

        public bool AgentAvailable
        {
            get
            {
                List<ChatUser> users = (new ChatDb()).GetUsers();
                var onlineAgent = users.Where(s => s.IsAvailable).FirstOrDefault();
                return (onlineAgent != null);
            }
        }

        public void PopulateCustomer(string name, string email)
        {
            customer.Name = name;
            customer.Email = email;
            customer.ConnectionId = Context.ConnectionId;
            var sessionId = (new ChatDb()).InsertSession(customer);

            if (agent.IsAvailable)
            {
                AgentConnection.showAvailableAgent(ActiveSessionsJson);
                //AgentConnection.showAcceptForChat();
                CustomerConnection.showStatusLine(
                    "Please wait for an agent to come online");
            }
            else
            {
                CustomerConnection.showStatusLine(
                    "No agent is currently available");
            }
        }

        public void AgentAccepts()
        {
            CustomerConnection.enableChat();
            CustomerConnection.showAgentAvailableMessage(agent.Name);
            isConnected = true;
        }

        private void CustomerRemove()
        {
            CustomerConnection.removeCookie();
            CustomerConnection.Abort();
            customer.Reset();
            isConnected = false;
        }

        public void AgentEndsChat()
        {
            if (!isConnected)
            {
                return;
            }
            CustomerConnection.showChatEndMessage();
            CustomerRemove();
        }

        public void CustomerEndsChat()
        {
            if (!isConnected)
            {
                return;
            }
            AgentConnection.showEndChatMessage();
            CustomerRemove();
        }

        public void Send(string name, string message, bool fromCustomer)
        {
            if (!isConnected) {
                return;
            }
            CustomerConnection.broadcastMessage(name, message, fromCustomer);
            AgentConnection.broadcastMessage(name, message, fromCustomer);
        }

        private string ActiveSessionsJson
        {
            get
            {
                var activeSessions = new List<string[]>();
                var chatSessions = (new ChatDb()).GetChatSessions(true);
                foreach (ChatSession chatSession in chatSessions)
                {
                    var strings = new string[3];
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


    }
}