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
                //sqlComm.Parameters.AddWithValue("@TimeRange", TimeRange);

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

    }
}