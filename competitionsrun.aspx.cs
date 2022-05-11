using FifaGame.ASPX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FifaGame
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!IsPostBack)
            {
                BindGridView();
           

            }
        }
        private void BindGridView()

        {

            DataTable dt = new DataTable();



            try

            {




                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Free_Competition ", webservice.dbc.conn);



                sqlDa.Fill(dt);

                if (dt.Rows.Count > 0)

                {

                    GridView1.DataSource = dt;

                    GridView1.DataBind();

                }

            }

            catch (System.Data.SqlClient.SqlException ex)

            {

                string msg = "Fetch Error:";

                msg += ex.Message;

                throw new Exception(msg);

            }

            finally

            {

                //connection.Close();

            }

        }

       

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "SendMail") return;
            int id = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            string date = row.Cells[2].Text;
            Run(id, date);
            
        }

        public void Run(int comp_id,string date)
        {
            string H = date.Split(':')[0];
            int hour = Convert.ToInt32(H);
            string M = date.Split(':')[1];
            int minute = Convert.ToInt32(M);
            int twinty_five = 3;
            int five = 2;

            Matches m = new Matches();
            m.FirstArrange(comp_id,date);
            int number_of_participants=check_number_of_participant(comp_id);

            ////////////////////////////////////////////////////////////////////////////////////////////////

            if (number_of_participants >= 2)
            {
                LabelRun.Text = comp_id + " Competition is running now";
                //Send Notification to All Users to be ready AT 18:00 and the ID of the contestance
                notification_firebase.SendToCustomCompetition send = new notification_firebase.SendToCustomCompetition();
                notification_firebase.SendToCustomUser send2 = new notification_firebase.SendToCustomUser();
                send.SendNotification("الرجاء الاستعداد ستبدا المسابقة في تمام الساعة " + date, comp_id.ToString());

            }
            else
            {
                LabelRun.Text = comp_id + "is not running because there is no enough participants";

            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            MyScheduler.IntervalInDays(hour, minute, 0,
      () => { //send to all to begin the round
          number_of_participants = check_number_of_participant(comp_id);
          if (number_of_participants >= 2)
          {
              Send_to_prepare();
          }

      });
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            minute = minute + twinty_five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //First Round Finished and send the noti for next match  (1)
            MyScheduler.IntervalInDays(hour, minute, 0, 
               () => {
                   int ho = DateTime.Now.Hour;
                   int mi = DateTime.Now.Minute;
                   int new_mi = mi + five;
                   if(new_mi>=60)
                   {
                       ho = ho + 1;
                       if (ho == 24)
                           ho = 0;
                       new_mi = new_mi - 60;
                   }
                   string time = "At " + ho + ":" + new_mi;
                   
                   number_of_participants = check_number_of_participant(comp_id);
                   if (number_of_participants >= 2)
                   {
                       Finish_The_Round_And_Send_Noti(comp_id,time);
                   }
               });
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            minute = minute + five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
///////////////////////////////////////////////////////////////////////////////////////////////////
            MyScheduler.IntervalInDays(hour, minute, 0,
           () => { //send to all to begin the second round   
           number_of_participants = check_number_of_participant(comp_id);
               if (number_of_participants >= 2)
               {
                   Send_to_prepare();
               }
           });
            minute = minute + twinty_five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////
            MyScheduler.IntervalInDays(hour, minute, 0,   //finish the second round (2)
                () => {
                number_of_participants = check_number_of_participant(comp_id);
                    int ho = DateTime.Now.Hour;
                    int mi = DateTime.Now.Minute;
                    int new_mi = mi + five;
                    if (new_mi >= 60)
                    {
                        ho = ho + 1;
                        new_mi = new_mi - 60;
                    }
                    string time = "At " + ho + ":" + new_mi;
                    if (number_of_participants >= 2)
                    {
                        Finish_The_Round_And_Send_Noti(comp_id,time);
                    }
                });
            minute = minute + five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            /////////////////////////////////////////////////////////////////////////////////
            MyScheduler.IntervalInDays(hour, minute, 0,
         () => { //send to all to begin the second round
         number_of_participants = check_number_of_participant(comp_id);
             if (number_of_participants >= 2)
             {
                 Send_to_prepare();
             }
         });
            minute = minute + twinty_five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////
            MyScheduler.IntervalInDays(hour, minute, 0, //finish the third round (3)
    () => {
    number_of_participants = check_number_of_participant(comp_id);
        if (number_of_participants >= 2)
        {
            int ho = DateTime.Now.Hour;
            int mi = DateTime.Now.Minute;
            int new_mi = mi + five;
            if (new_mi >= 60)
            {
                ho = ho + 1;
                if (ho == 24)
                    ho = 0;
                new_mi = new_mi - 60;
            }
            string time = "At " + ho + ":" + new_mi;
            Finish_The_Round_And_Send_Noti(comp_id,time);
        }
    });
            minute = minute + five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            /////////////////////////////////////////////////////////////////////////////////
            MyScheduler.IntervalInDays(hour, minute, 0,
  () => { //send to all to begin the second round
  number_of_participants = check_number_of_participant(comp_id);
      if (number_of_participants >= 2)
      {
          Send_to_prepare();
      }
  });
            minute = minute + twinty_five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////
            MyScheduler.IntervalInDays(hour, minute, 0, //finish the fourth round (4)
() => {
number_of_participants = check_number_of_participant(comp_id);
    if (number_of_participants >= 2)
    {
        int ho = DateTime.Now.Hour;
        int mi = DateTime.Now.Minute;
        int new_mi = mi + five;
        if (new_mi >= 60)
        {
            ho = ho + 1;
            if (ho == 24)
                ho = 0;
            new_mi = new_mi - 60;
        }
        string time = "At " + ho + ":" + new_mi;
        Finish_The_Round_And_Send_Noti(comp_id,time);
    }
});
            minute = minute + five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            ///////////////////////////////////////////////////////////////////////////////// 
            MyScheduler.IntervalInDays(hour, minute, 0,   
() => { //send to all to begin the second round
number_of_participants = check_number_of_participant(comp_id);
    if (number_of_participants >= 2)
    {
        Send_to_prepare();
    }
});
            minute = minute + twinty_five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////
                   MyScheduler.IntervalInDays(hour, minute, 0,  //finish the fifth round (5)
            () => {
            number_of_participants = check_number_of_participant(comp_id);
                if (number_of_participants >= 2)
                {
                    int ho = DateTime.Now.Hour;
                    int mi = DateTime.Now.Minute;
                    int new_mi = mi + five;
                    if (new_mi >= 60)
                    {
                        ho = ho + 1;
                        if (ho == 24)
                            ho = 0;
                        new_mi = new_mi - 60;
                    }
                    string time = "At " + ho + ":" + new_mi;
                    Finish_The_Round_And_Send_Noti(comp_id,time);
                }
            });
            minute = minute + five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            ///////////////////////////////////////////////////////////////////////////////// 
            MyScheduler.IntervalInDays(hour, minute, 0,
() => { //send to all to begin the second round
number_of_participants = check_number_of_participant(comp_id);
    if (number_of_participants >= 2)
    {
        Send_to_prepare();
    }
});
            minute = minute + twinty_five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////
            MyScheduler.IntervalInDays(hour, minute, 0,  //finish the sixth round (6)
() => {
number_of_participants = check_number_of_participant(comp_id);
    if (number_of_participants >= 2)
    {
        int ho = DateTime.Now.Hour;
        int mi = DateTime.Now.Minute;
        int new_mi = mi + five;
        if (new_mi >= 60)
        {
            ho = ho + 1;
            if (ho == 24)
                ho = 0;
            new_mi = new_mi - 60;
        }
        string time = "At " + ho + ":" + new_mi;
        Finish_The_Round_And_Send_Noti(comp_id,time);
    }
});
            minute = minute + five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            ///////////////////////////////////////////////////////////////////////////////// 
                 MyScheduler.IntervalInDays(hour, minute, 0,
            () => { //send to all to begin the second round
            number_of_participants = check_number_of_participant(comp_id);
                if (number_of_participants >= 2)
                {
                    Send_to_prepare();
                }
            });
            minute = minute + twinty_five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////
                       MyScheduler.IntervalInDays(hour, minute, 0,  //finish the seventh round (7)
            () => {
            number_of_participants = check_number_of_participant(comp_id);
                if (number_of_participants >= 2)
                {
                    int ho = DateTime.Now.Hour;
                    int mi = DateTime.Now.Minute;
                    int new_mi = mi + five;
                    if (new_mi >= 60)
                    {
                        ho = ho + 1;
                        if (ho == 24)
                            ho = 0;
                        new_mi = new_mi - 60;
                    }
                    string time = "At " + ho + ":" + new_mi;
                    Finish_The_Round_And_Send_Noti(comp_id,time);
                }
            });
            minute = minute + five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            ///////////////////////////////////////////////////////////////////////////////// 
        }
        public int check_number_of_participant(int comp_id)
        {
            DataTable rt1 = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Free_Compitition_Participants where compitition_id=" + comp_id, webservice.dbc.conn);

            adapter.Fill(rt1);
            int number_of_participants = rt1.Rows.Count;
            return number_of_participants;
        }
        public void Finish_The_Round_And_Send_Noti(int comp_id,string time)
        {
            notification_firebase.SendToCustomUser send2 = new notification_firebase.SendToCustomUser();
            Console.WriteLine("//here write the code that you want to schedule");
            Matches m1 = new Matches();
            m1.DeleteTheLoosers(comp_id,time);
            DataTable rt1 = new DataTable();
            Console.WriteLine("//here write the code that you want to schedule");
            //Send Notification to All Users to be ready at new Date and the ID of the contestance
            SqlDataAdapter adapter2 = new SqlDataAdapter("select * from Free_Compitition_Participants ", webservice.dbc.conn);
            adapter2.Fill(rt1);
            foreach (DataRow row in rt1.Rows)
            {
                int user_id = row.Field<int>("user_id");
                send2.SendNotification("  الرجاء الاستعداد لمباراتك القادمة بعد خمس دقائق انذهب الى صفحة مبارايتي   ", user_id.ToString());

            }
        }
        public void Send_to_prepare()
        {
            notification_firebase.SendToCustomUser send2 = new notification_firebase.SendToCustomUser();
            DataTable rt1 = new DataTable();
            Console.WriteLine("//here write the code that you want to schedule");
            //Send Notification to All Users to be ready at new Date and the ID of the contestance
            SqlDataAdapter adapter2 = new SqlDataAdapter("select * from Free_Compitition_Participants ", webservice.dbc.conn);
            adapter2.Fill(rt1);
            foreach (DataRow row in rt1.Rows)
            {
                int user_id = row.Field<int>("user_id");
                send2.SendNotification("الرجاء الاستعداد لمباراتك القادمة الان  ", user_id.ToString());

            }
        }

    }
}