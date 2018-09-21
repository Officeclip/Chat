using Microsoft.AspNet.SignalR;

namespace OfficeClip.LiveChat.Chat
{
    public class ChatHub : Hub
    {
        protected static ChatUser agent = new ChatUser();
        protected static ChatUser customer = new ChatUser();
        protected static bool isConnected = false;

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

        public void PopulateAgent(string name, string email)
        {
            agent.Name = name;
            agent.Email = email;
            agent.ConnectionId = Context.ConnectionId;
            AgentConnection.showWaitingForChat();
        }

        public void PopulateCustomer(string name, string email)
        {
            customer.Name = name;
            customer.Email = email;
            customer.ConnectionId = Context.ConnectionId;
            if (agent.IsAvailable)
            {
                AgentConnection.showAcceptForChat();
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
    }
}