using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Encrypt.Decrypt.Library;

namespace TimeClock
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            string userID;
            string hours = HourBox.Text.PadLeft(2, '0');
            string minutes = MinuteBox.Text.PadLeft(2, '0');
            string adjustReason = ReasonBox.Text;
            if (null != Page.Request["ID"])
            {
                userID = Page.Request["ID"];
            }
            else
            {
                userID = "";
            }

            if (hours == null || hours == "")
            {
                hours = "00";
            }
            if (minutes == null || minutes == "")
            {
                minutes = "00";
            }

            MySqlCommand mySqlCommand;
            MySqlConnection connection = null;
            string connStr = AutoMap_Crypt.Decrypt_Slowly(@"IfdWvr7g / AMMaXAvBBXjsRIOn3 +/ P8HLjbCjIOwSUFhilKE2Bc + 7qSxJe1UxVRKm7AlmWqKMaOYyg7HpPfYV79Auwj0u0oC + XOidiIrsqYtNGd4njyAS6g ==");
            connStr = connStr + "ScottWork";
            string sqlStr = "";
            connection = new MySqlConnection(connStr);
            MySqlDataReader mySqlDataReader;

            sqlStr = "INSERT INTO ScottWork.TimeStamps  VALUES ('" + adjustReason + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + hours + ":" + minutes + ":00', '" + userID + "')";

            mySqlCommand = new MySqlCommand(sqlStr, connection);
            connection.Open();
            mySqlDataReader = mySqlCommand.ExecuteReader();

            //Closing everything down like a good boy.
            mySqlDataReader.Close();
            connection.Close();
            HourBox.Text = "";
            MinuteBox.Text = "";
            FeedbackLabel.Text = "Time Adjusted!";

        }

        protected void SubtractButton_Click(object sender, EventArgs e)
        {
            string userID;
            string hours = HourBox.Text.PadLeft(2, '0');
            string minutes = MinuteBox.Text.PadLeft(2, '0');
            string adjustReason = ReasonBox.Text;
            if (null != Page.Request["ID"])
            {
                userID = Page.Request["ID"];
            }
            else
            {
                userID = "";
            }

            if(hours == null || hours == "")
            {
                hours = "00";
            }
            if (minutes == null || minutes == "")
            {
                minutes = "00";
            }

            MySqlCommand mySqlCommand;
            MySqlConnection connection = null;
            string connStr = AutoMap_Crypt.Decrypt_Slowly(@"IfdWvr7g / AMMaXAvBBXjsRIOn3 +/ P8HLjbCjIOwSUFhilKE2Bc + 7qSxJe1UxVRKm7AlmWqKMaOYyg7HpPfYV79Auwj0u0oC + XOidiIrsqYtNGd4njyAS6g ==");
            connStr = connStr + "ScottWork";
            string sqlStr = "";
            connection = new MySqlConnection(connStr);
            MySqlDataReader mySqlDataReader;

            sqlStr = "INSERT INTO ScottWork.TimeStamps  VALUES ('" + adjustReason + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '-" + hours + ":" + minutes + ":00', '" + userID + "')";

            mySqlCommand = new MySqlCommand(sqlStr, connection);
            connection.Open();
            mySqlDataReader = mySqlCommand.ExecuteReader();

            //Closing everything down like a good boy.
            mySqlDataReader.Close();
            connection.Close();
            FeedbackLabel.Text = "Time Adjusted!";
            HourBox.Text = "";
            MinuteBox.Text = "";
            //Just one more change
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            string ShutDown = "window.close()";

            this.Controls.Add(new LiteralControl("<Script Language=javascript>"));
            this.Controls.Add(new LiteralControl(ShutDown));
            this.Controls.Add(new LiteralControl("</Script>"));
        }
    }
}