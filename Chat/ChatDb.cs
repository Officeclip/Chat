using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace OfficeClip.LiveChat.Chat
{
    public class ChatDb
    {
        private static string connectionString = GetConnectionString();
        private static string GetConnectionString()
        {
            var dbString = WebConfigurationManager.ConnectionStrings["DbString"];
            return dbString?.ToString() ?? string.Empty;
        }

        public List<ChatUser> GetUsers()
        {
            List<ChatUser> chatUsers = new List<ChatUser>();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlComm = new SqlCommand("GetUsersSP", conn);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }
            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                ChatUser chatUser = new ChatUser()
                {
                    Name = (string)dRow["Name"],
                    Email = (string)dRow["Email"]
                };
                chatUsers.Add(chatUser);
            }
            return chatUsers;
        }

        public List<ChatUser> GetEndUsers()
        {
            List<ChatUser> chatUsers = new List<ChatUser>();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlComm = new SqlCommand("GetEndUsersSP", conn);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }
            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                ChatUser chatUser = new ChatUser()
                {
                    Name = (string)dRow["Name"],
                    Email = (string)dRow["Email"]
                };
                chatUsers.Add(chatUser);
            }
            return chatUsers;
        }

        public List<ChatSession> GetChatSessions(bool isActive)
        {
            List<ChatSession> chatSessions = new List<ChatSession>();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlComm = new SqlCommand("GetEndUsersSP", conn);
                sqlComm.Parameters.Add(
                    new SqlParameter("@isActive", isActive));
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }
            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                ChatSession chatSession = new ChatSession()
                {
                    ChatSessionId = Convert.ToInt32(dRow["chatSessionId"]),
                    EndUserName = (string)dRow["Name"],
                    EndUserEmail = (string)dRow["Email"],
                    StartTime = (Convert.IsDBNull(dRow["startTime"]))
                                    ? (DateTime?)null
                                    : Convert.ToDateTime(dRow["startTime"]),
                    EndTime = (Convert.IsDBNull(dRow["endTime"]))
                                    ? (DateTime?)null
                                    : Convert.ToDateTime(dRow["endTime"]),
                    EndUserMode = (EndUserMode)Convert.ToInt32(dRow["mode"])
                };
                chatSessions.Add(chatSession);
            }
            return chatSessions;
        }


    }
}