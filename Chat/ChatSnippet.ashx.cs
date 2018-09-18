using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeClip.LiveChat.Chat
{
    /// <summary>
    /// Summary description for ChatSnippet1
    /// </summary>
    public class ChatSnippet : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string snippet = @"
                var chatImageSpan = document.getElementById('chatImageSpan');  
                chatImageSpan.innerHTML = ""<a href='javascript:void(0);' onclick='javascript:window.open(chatRoot + \""customer.html\"",\""\"",\""width=400px,height=660px,toolbar=no,resizable=yes,scrollbars=yes,directories=no,menubar=no,location=no\"")'><img src='' border='0' style='cursor:pointer' alt='ChatImage' id='ChatImage' name='ChatImage'/></a>"";  
                var chatImage = document.getElementById('ChatImage');
                chatImage.src = {0};
            ";
            var jsString = string.Format(snippet, "offlineImage");
            context.Response.ContentType = "application/javascript";
            context.Response.Write(jsString);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}