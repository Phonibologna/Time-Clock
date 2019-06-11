using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Encrypt.Decrypt.Library;
using TimeClock.Model;

namespace TimeClock
{


    public partial class Mainpage : System.Web.UI.Page
    {
        

        protected static DateTime _lastPunchInTime;
        protected static DateTime _selectedDate;
        protected static TimeSpan _span;
        protected static TimeSpan _totalTime;
        protected static string userID;

        

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if there are timestamps for today, if not, assign a value to totalTime so it is not null;
            //Otherwise, get total time worked so far from timestamps.
            if (!IsPostBack)
            {
                if(Request.Cookies["TimeClock"] != null)
                {
                    IDBox.Text = Request.Cookies["TimeClock"].Value;
                }
            }
            else
            {
                Response.Cookies.Add(new HttpCookie("TimeClock", IDBox.Text));
            }

            if (userID != null)
            {
                if (GetTotalTime() == null)
                {
                    _totalTime = new TimeSpan(0, 0, 0, 0);
                }
                else
                {

                    _totalTime = GetTotalTime();
                    TimeLabel.Text = _totalTime.ToString(@"h\:mm\:ss");

                }


                //Check current status in regards to being punched in or not.
                if (GetLastInOut() == "In")
                {
                    StatusLabel.Text = "Working";
                }
                else
                {
                    StatusLabel.Text = "Not Working";
                }

            }
        }

        

        protected void PunchInButton_Click(object sender, EventArgs e)
        {
            //Start Declaring Local Variables

            MySqlCommand mySqlCommand;
            MySqlConnection connection = null;
            _lastPunchInTime = DateTime.Now;
            userID = IDBox.Text;

            string connStr = AutoMap_Crypt.Decrypt_Slowly(@"IfdWvr7g / AMMaXAvBBXjsRIOn3 +/ P8HLjbCjIOwSUFhilKE2Bc + 7qSxJe1UxVRKm7AlmWqKMaOYyg7HpPfYV79Auwj0u0oC + XOidiIrsqYtNGd4njyAS6g ==");
            connStr = connStr + "ScottWork";
            string sqlStr = "";
            connection = new MySqlConnection(connStr);

            MySqlDataReader mySqlDataReader;
            //This string makes a timestamp that states it is a "punch in" and the current time.
            sqlStr = "INSERT INTO ScottWork.TimeStamps  VALUES ('In', '"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '', '" + userID +"')";
            mySqlCommand = new MySqlCommand(sqlStr, connection);

            connection.Open();
            mySqlDataReader = mySqlCommand.ExecuteReader();
            //Close up shop and set the status to show that the user is now punched in.
            mySqlDataReader.Close();
            connection.Close();
            StatusLabel.Text = "Working";
            LastPunchLbl.Text = DateTime.Now.ToString();

            PunchOutButton.Enabled = true;
            PunchInButton.Enabled = false;



        }

        protected void PunchOutButton_Click(object sender, EventArgs e)
        {
            //basically the same as punch in, but with a twist.
            //PunchOutTime and Span are used to calculate total time worked so far because i'm a hack.
            MySqlCommand mySqlCommand;
            MySqlConnection connection = null;

            DateTime PunchOutTime = DateTime.Now;
            _span = PunchOutTime.Subtract(GetLastPunchIn());
            userID = IDBox.Text;
            
            
            //A whole bunch of sql stuff
            string connStr = AutoMap_Crypt.Decrypt_Slowly(@"IfdWvr7g / AMMaXAvBBXjsRIOn3 +/ P8HLjbCjIOwSUFhilKE2Bc + 7qSxJe1UxVRKm7AlmWqKMaOYyg7HpPfYV79Auwj0u0oC + XOidiIrsqYtNGd4njyAS6g ==");
            connStr = connStr + "ScottWork";
            string sqlStr = "";
            connection = new MySqlConnection(connStr);
            MySqlDataReader mySqlDataReader;
            //Makes a timestamp for clocking out, with values of Out, the current date and time, and the time since the last punch in.
            sqlStr = "INSERT INTO ScottWork.TimeStamps VALUES ('Out', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + _span.Duration().ToString() + "', '" + userID + "')";
            mySqlCommand = new MySqlCommand(sqlStr, connection);
            connection.Open();
            mySqlDataReader = mySqlCommand.ExecuteReader();
            //Close it all up again
            mySqlDataReader.Close();
            connection.Close();

            PunchOutButton.Enabled = false;
            PunchInButton.Enabled = true;

            _totalTime = GetTotalTime();
            TimeLabel.Text = _totalTime.ToString(@"h\:mm\:ss");
            StatusLabel.Text = "Not Working";
            LastPunchLbl.Text = DateTime.Now.ToString();
            //Assigns total time worked to totalTime, and then outputs it to the user.
            //I'm sure I had a good reason for making it a global variable, but I can't remember that reason anymore.

        }
        //Make a timestamp to adjust the total time worked for various reasons.
        protected void OtherButton_Click(object sender, EventArgs e)
        {
            userID = IDBox.Text;
            string nextPage = "window.open('TimeAdjustment.aspx?ID=" + userID + "', '_blank')";

            this.Controls.Add(new LiteralControl("<Script Language=javascript>"));
            this.Controls.Add(new LiteralControl(nextPage));
            this.Controls.Add(new LiteralControl("</Script>"));

            


        }

        protected void OtherTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void AdjustTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        public string GetLastInOut()
        {
            MySqlCommand mySqlCommand;
            MySqlConnection connection = null;
            //These variables are used to get timestamps for the current day.
            TimeSpan AddSpan = new TimeSpan(1, 0, 0, 0);
            DateTime Date1 = DateTime.Now;
            DateTime Date2 = DateTime.Now + AddSpan;
            string getInOut = "";
            userID = IDBox.Text;

            string connStr = AutoMap_Crypt.Decrypt_Slowly(@"IfdWvr7g / AMMaXAvBBXjsRIOn3 +/ P8HLjbCjIOwSUFhilKE2Bc + 7qSxJe1UxVRKm7AlmWqKMaOYyg7HpPfYV79Auwj0u0oC + XOidiIrsqYtNGd4njyAS6g ==");
            connStr = connStr + "ScottWork";
            string sqlStr = "";
            connection = new MySqlConnection(connStr);

            MySqlDataReader mySqlDataReader;
            //Apparently InOut is already used for something else. Whoops. Make sure it always has `` around it.
            sqlStr = "SELECT `InOut` FROM ScottWork.TimeStamps WHERE `InOut` = 'In' OR `InOut` = 'Out' AND userID = '" + userID +"' AND Time>'" + Date1.ToString("yyyy-MM-dd") + "' AND Time<'" + Date2.ToString("yyyy-MM-dd") + "' ORDER BY Time DESC LIMIT 1";

            mySqlCommand = new MySqlCommand(sqlStr, connection);
            connection.Open();
            mySqlDataReader = mySqlCommand.ExecuteReader();
            
            //get the most recent InOut that is not an adjustment and assign it to getInOut.
            if (mySqlDataReader.Read())
            {
                getInOut = mySqlDataReader.GetString("InOut");
            }

            //Close it up and return the value that was just gotten.
            mySqlDataReader.Close();
            connection.Close();

            return getInOut;
        }

        public DateTime GetLastPunchIn()
        {

            //this is basically the same thing as the above, but it gets the last time the user punched in for various reasons.
            MySqlCommand mySqlCommand;
            MySqlConnection connection = null;


            TimeSpan AddSpan = new TimeSpan(1, 0, 0, 0);
            DateTime CalendarDate = DateTime.Now;
            DateTime CalendarDate2 = DateTime.Now + AddSpan;
            DateTime LastPunchInDate = DateTime.Now;
            userID = IDBox.Text;



            string connStr = AutoMap_Crypt.Decrypt_Slowly(@"IfdWvr7g / AMMaXAvBBXjsRIOn3 +/ P8HLjbCjIOwSUFhilKE2Bc + 7qSxJe1UxVRKm7AlmWqKMaOYyg7HpPfYV79Auwj0u0oC + XOidiIrsqYtNGd4njyAS6g ==");
            connStr = connStr + "ScottWork";
            string sqlStr = "";
            connection = new MySqlConnection(connStr);

            MySqlDataReader mySqlDataReader;

            sqlStr = "SELECT Time FROM ScottWork.TimeStamps WHERE `InOut`='In' AND userID = '" + userID + "' AND Time>'" + CalendarDate.ToString("yyyy-MM-dd") + "' AND Time<'" + CalendarDate2.ToString("yyyy-MM-dd") + "' ORDER BY Time DESC LIMIT 1";

            mySqlCommand = new MySqlCommand(sqlStr, connection);


            connection.Open();
            mySqlDataReader = mySqlCommand.ExecuteReader();

            if(mySqlDataReader.Read())
            {
                LastPunchInDate = mySqlDataReader.GetDateTime("Time");
            }




            mySqlDataReader.Close();
            connection.Close();

            

            return LastPunchInDate;
        }

        public TimeSpan GetTotalTime()
        {
            

            TimeSpan TrueTotalTime = new TimeSpan(0, 0, 0, 0);
            TimeSpan TableTime;
            MySqlCommand mySqlCommand;
            MySqlConnection connection = null;
            string connStr = AutoMap_Crypt.Decrypt_Slowly(@"IfdWvr7g / AMMaXAvBBXjsRIOn3 +/ P8HLjbCjIOwSUFhilKE2Bc + 7qSxJe1UxVRKm7AlmWqKMaOYyg7HpPfYV79Auwj0u0oC + XOidiIrsqYtNGd4njyAS6g ==");
            connStr = connStr + "ScottWork";
            string sqlStr = "";
            connection = new MySqlConnection(connStr);
            userID = IDBox.Text;

            MySqlDataReader mySqlDataReader;

            sqlStr = "SELECT TimeIncrement FROM ScottWork.TimeStamps WHERE `InOut`!='In' AND userID = '" + userID + "' AND Time>'" + DateTime.Now.ToString("yyyy-MM-dd") +"' ";

            mySqlCommand = new MySqlCommand(sqlStr, connection);


            connection.Open();
            mySqlDataReader = mySqlCommand.ExecuteReader();
            //Add all timestamps that are from today and are either Out or an Adjustment.
            while (mySqlDataReader.Read())
            {
                TableTime = TimeSpan.Parse(mySqlDataReader.GetString("TimeIncrement"));
                TrueTotalTime = TrueTotalTime + TableTime;
            }


            mySqlDataReader.Close();
            connection.Close();

            return TrueTotalTime;
        }

        protected void WorkCalendar_SelectionChanged(object sender, EventArgs e)
        {
            //This allows the user to view time worked for previous days by clicking a date on the calendar.

            //these variables should be familiar by now.
            userID = IDBox.Text;
            TimeSpan TrueTotalTime = new TimeSpan(0, 0, 0, 0);
            TimeSpan TableTime;
            TimeSpan AddSpan = new TimeSpan(1, 0, 0, 0);
            DateTime CalendarDate = WorkCalendar.SelectedDate;
            DateTime CalendarDate2 = WorkCalendar.SelectedDate + AddSpan;
            

            //same with these.
            MySqlCommand mySqlCommand;
            MySqlConnection connection = null;
            string connStr = AutoMap_Crypt.Decrypt_Slowly(@"IfdWvr7g / AMMaXAvBBXjsRIOn3 +/ P8HLjbCjIOwSUFhilKE2Bc + 7qSxJe1UxVRKm7AlmWqKMaOYyg7HpPfYV79Auwj0u0oC + XOidiIrsqYtNGd4njyAS6g ==");
            connStr = connStr + "ScottWork";
            string sqlStr = "";
            connection = new MySqlConnection(connStr);

            MySqlDataReader mySqlDataReader;
            //grabs all timestamps that are not In stamps and are from the selected date.
            sqlStr = "SELECT TimeIncrement FROM ScottWork.TimeStamps WHERE `InOut` != 'In' AND userID = '" + userID + "' AND Time>'" + CalendarDate.ToString("yyyy-MM-dd") + "' AND Time<'" + CalendarDate2.ToString("yyyy-MM-dd") + "' ";

            mySqlCommand = new MySqlCommand(sqlStr, connection);


            connection.Open();
            mySqlDataReader = mySqlCommand.ExecuteReader();
            //Adds all the timestamps to get total worked time for that day.
            while (mySqlDataReader.Read())
            {
                TableTime = TimeSpan.Parse(mySqlDataReader.GetString("TimeIncrement"));
                TrueTotalTime = TrueTotalTime + TableTime;
            }



            //Close everything up one final time.
            mySqlDataReader.Close();
            connection.Close();
            TimeLabel.Text = TrueTotalTime.ToString(@"h\:mm\:ss");

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            TimeSpan SemiTotalTime = GetTotalTime();
            DateTime LastPunchIn = GetLastPunchIn();
            TimeSpan AddTime = DateTime.Now - LastPunchIn;
            TimeSpan FinalTotalTime = SemiTotalTime + AddTime;

            TimeLabel.Text = FinalTotalTime.ToString(@"h\:mm\:ss");
        }
    }


    
}