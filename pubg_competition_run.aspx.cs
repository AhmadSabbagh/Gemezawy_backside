using FifaGame.ASPX;
using FifaGame.webservice;
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
    public partial class pubg_competition_run : System.Web.UI.Page
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




                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Pubg_Free_Competition ", webservice.dbc.conn);



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
            if (roompassTXT.Text.Equals("")) return;
            if (roomnameTXT.Text.Equals("")) return;
            int id = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            string date = row.Cells[2].Text;
            string room_name = roomnameTXT.Text;
            string room_password = roompassTXT.Text;

            Run(id, date,room_name,room_password);
        }
        public void Run(int comp_id, string date,string room_name,string room_pass)
        {
            string H = date.Split(':')[0];
            int hour = Convert.ToInt32(H);
            string M = date.Split(':')[1];
            int minute = Convert.ToInt32(M);
            int twinty_five = 3;
            //int five = 2;

            Matches m = new Matches();
            int number_of_participants = check_number_of_participant(comp_id);

            if (number_of_participants >= 2)
            {
                LabelRun.Text = comp_id + " Competition is running now";
                //Send Notification to All Users to be ready AT 18:00 and the ID of the contestance
                notification_firebase.SendToCustomPubgComp send = new notification_firebase.SendToCustomPubgComp();
                notification_firebase.SendToCustomUser send2 = new notification_firebase.SendToCustomUser();
                send.SendNotification("الرجاء الاستعداد ستبدا المسابقة في تمام الساعةس احرص على تدوين المعلومات ادناه " + date + " , room's name : "+room_name +" , Room's Password: "+room_pass, comp_id.ToString());

            }
            else
            {
                LabelRun.Text = comp_id + "is not running because there is no enough participants";

            }
            MyScheduler.IntervalInDays(hour, minute, 0,
                  () => { //send to all to begin the round
      
        Send_to_prepare(comp_id);
    

                         });
            minute = minute + twinty_five;
            if (minute >= 60)
            {
                hour = hour + 1;
                if (hour == 24)
                    hour = 0;
                minute = minute - 60;
            }
            MyScheduler.IntervalInDays(hour, minute, 0,
                  () => { //send to all to begin the round
                      string time = "At " + hour + ":" + minute;
                      Delete_losers_assign_the_winners(comp_id,time);


                  });

        }

        public void Send_to_prepare(int comp_id)
        {
            notification_firebase.SendToCustomPubgComp send = new notification_firebase.SendToCustomPubgComp();
            notification_firebase.SendToCustomUser send2 = new notification_firebase.SendToCustomUser();
            send.SendNotification("المسابقة بدأت الان ",comp_id.ToString());

        }
        public int check_number_of_participant(int comp_id)
        {
            DataTable rt1 = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Pubg_Free_Competition_Participants where competition_id=" + comp_id, webservice.dbc.conn);

            adapter.Fill(rt1);
            int number_of_participants = rt1.Rows.Count;
            return number_of_participants;
        }
        public void Delete_losers_assign_the_winners(int comp_id,string time)
        {
            string mess = null;
            int result = 0;
   
            string sql1 = "";
         

            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();

            try
            {


                /////////////////////////////////////////////////////////////////////////////////////


                /////////////////////////////////////////////////////////////////////////////////////
                sql1 = "delete from Pubg_Free_Competition_Participants where winner_flag=0 and competition_id=" + comp_id;
             



                SqlCommand cmd2 = new SqlCommand(sql1, dbc.conn);
                dbc.conn.Open();
                result = cmd2.ExecuteNonQuery();
              



                mess = "delete succ ";
                dbc.conn.Close();
                //  arrange(comp_id, time);

                SqlDataAdapter adapter2 = new SqlDataAdapter("select user_id from Pubg_Free_Competition_Participants where winner_flag=1 and competition_id=" + comp_id, webservice.dbc.conn);
                adapter2.Fill(rt1);
                foreach (DataRow row in rt1.Rows)
                {
                    int user_id = row.Field<int>("user_id");
                    // send2.SendNotification("الرجاء الاستعداد لمباراتك القادمة الان  ", user_id.ToString());
                    string sql = "insert into Free_Winners (winner_id,competition_id,date,game) values" +
                                  " (" + user_id + "," + comp_id + ",'" + time + "','PUBG')";
                    string sql3 = "delete from Pubg_Free_Competition_Participants";
                    SqlCommand cmd1 = new SqlCommand(sql3, dbc.conn);
                    SqlCommand cmd6 = new SqlCommand(sql, dbc.conn);
                    dbc.conn.Open();
                   int  result5 = cmd1.ExecuteNonQuery();
                   int  result2 = cmd6.ExecuteNonQuery();
                    dbc.conn.Close();
                    if (result != 0 && result2 != 0)
                    {
                        mess = "succ ";

                    }
                    notification_firebase.SendToCustomUser send_congrats = new notification_firebase.SendToCustomUser();
                    send_congrats.SendNotification("مبروك فوزك في المسابقة", user_id.ToString());
                }



            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "تأكد من صحة البيانات ";
            }

        }
    }
}