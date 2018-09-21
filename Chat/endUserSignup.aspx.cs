using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OfficeClip.LiveChat.Chat
{
    public partial class endUserSignup : System.Web.UI.Page
    {
        public bool IsAgentAvailable = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            imgLogo.Src = "~/Images/logo.gif";
            Img1.Src =
                (!IsAgentAvailable)
                ? "~/Images/alert.png"
                : "~/Images/chat.png";
            topMessage.Text =
                (!IsAgentAvailable)
                ? "No agents are available. Please leave a message"
                : "Please fill-in this form to start a chat";
            topMessage.ForeColor =
                (!IsAgentAvailable)
                ? System.Drawing.Color.Red
                : System.Drawing.Color.Green;
            btnSend.Attributes.Add("onclick", "return btnClick();");
        }
        protected void btnSendClick(object sender, EventArgs e)
        {
            Response.Redirect("endUserChat.aspx");
        }
    }
}