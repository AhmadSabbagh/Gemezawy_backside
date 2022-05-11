using FifaGame.webservice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FifaGame.ASPX
{
    public class Matches
    {

       public void FirstArrange (int comp_id,string date)
        {
            string mess = null;
            string mess2 = null;
            int result = 0;
            int result2 = 0;
            string sql = "";
            string sql2 = "";
            int id = 0;
            List<int> Temp_user_id = new List<int>();
            List<string> Temp_user_name = new List<string>();

            DataTable rt1 = new DataTable();
            DataTable rt12 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Free_Compitition_Participants where compitition_id=" + comp_id, dbc.conn);
                adapter.Fill(rt1);
                int number_of_participants = rt1.Rows.Count;
                int diffrence = 0;
                if (rt1 != null && number_of_participants >= 128)
                {
                    diffrence = number_of_participants - 128;
                    send_appolgy(diffrence, comp_id);
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants  where compitition_id=" + comp_id+"  ORDER BY user_id DESC)";

                    number_of_participants = number_of_participants - diffrence;

                }
                else if (rt1 != null && number_of_participants >= 64)
                {

                    diffrence = number_of_participants - 64;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants  where compitition_id=" + comp_id + "  ORDER BY user_id DESC)";
                    send_appolgy(diffrence, comp_id);

                    number_of_participants = number_of_participants - diffrence;

                }
                else if (rt1 != null && number_of_participants >= 32)
                {

                    diffrence = number_of_participants - 32;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants  where compitition_id=" + comp_id + "  ORDER BY user_id DESC)";
                    send_appolgy(diffrence, comp_id);

                    number_of_participants = number_of_participants - diffrence;

                }
                else if (rt1 != null && number_of_participants >= 16)
                {

                    diffrence = number_of_participants - 16;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants  where compitition_id=" + comp_id + "  ORDER BY user_id DESC)";
                    send_appolgy(diffrence, comp_id);

                    number_of_participants = number_of_participants - diffrence;

                }
                else if (rt1 != null && number_of_participants >= 8)
                {

                    diffrence = number_of_participants - 8;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants  where compitition_id=" + comp_id + "  ORDER BY user_id DESC)";
                    number_of_participants = number_of_participants - diffrence;
                    send_appolgy(diffrence, comp_id);


                }
                else if (rt1 != null && number_of_participants >= 4)
                {

                    diffrence = number_of_participants - 4;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants  where compitition_id=" + comp_id + "   ORDER BY user_id DESC)";

                    number_of_participants = number_of_participants - diffrence;
                    send_appolgy(diffrence, comp_id);

                }

                else if (rt1 != null && number_of_participants >= 2)
                {

                    diffrence = number_of_participants - 2;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants where compitition_id=" + comp_id +" ORDER BY user_id DESC)";
                    number_of_participants = number_of_participants - diffrence;
                    send_appolgy(diffrence, comp_id);


                }
                else
                {
                    
                    sql = "Delete from Free_Compitition_Participants ";
                    // there is only one participant or no participant

                }

                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                dbc.conn.Open();
                result = cmd.ExecuteNonQuery();
                dbc.conn.Close();
                if (result != 0)
                {
                    mess = "delete succ ";

                }
                if(number_of_participants>=2)
                {
                    SqlDataAdapter adapter2 = new SqlDataAdapter("select * from Free_Compitition_Participants where compitition_id=" + comp_id , dbc.conn);

                    adapter2.Fill(rt12);
                    int[] temp_user_id2 = new int[number_of_participants];
                    string[] temp_user_name2 = new string[number_of_participants];

                    foreach (DataRow row in rt12.Rows)
                    {
                        int user_id = row.Field<int>("user_id");
                        string user_name= row.Field<string>("user_name");
                        Temp_user_id.Add(user_id);
                        Temp_user_name.Add(user_name);
                     


                    }
                   temp_user_id2= Temp_user_id.ToArray();
                    temp_user_name2 = Temp_user_name.ToArray();

                        for (int i = 0; i < Temp_user_id.Count; i = i + 2)
                    {
                        sql2 = "insert into Free_Rounds (user_id1,user_id2,date,winner_id,loser_id,compitition_id,user_name1,user_name2) values" +
                                      " (" + temp_user_id2[i] + "," + temp_user_id2[i+1]+ ",'" + date + "',0,0,"+comp_id+ ",'" + temp_user_name2[i] + "','" + temp_user_name2[i + 1] + "' )";
                        SqlCommand cmd1 = new SqlCommand(sql2, dbc.conn);
                        dbc.conn.Open();
                        result = cmd1.ExecuteNonQuery();
                        dbc.conn.Close();
                        if (result != 0)
                        {
                            mess2 = "succ ";

                        }
                    }

                    
                }
                


            }




            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = " Data Incorrect ";
            }
        }
        public void arrange(int comp_id,string time)
        {

            string mess = null;
            string mess2 = null;
            int result = 0;
            int result2 = 0;
            int  result5 =0;
            string sql = "";
            string sql2 = "";
            string sql3 = "";
            int id = 0;
            int should_continue= 1;
            List<int> Temp_user_id = new List<int>();
            List<string> Temp_user_name = new List<string>();
            DataTable rt1 = new DataTable();
            DataTable rt12 = new DataTable();

            ReturnData rt = new ReturnData();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Free_Compitition_Participants  where compitition_id=" + comp_id , dbc.conn);

                adapter.Fill(rt1);
                int number_of_participants = rt1.Rows.Count;
                int diffrence = 0;
                if (rt1 != null && number_of_participants >= 128)
                {
                    diffrence = number_of_participants - 128;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants where compitition_id=" + comp_id +" ORDER BY user_id DESC)";
                    number_of_participants = number_of_participants - diffrence;

                }
                else if (rt1 != null && number_of_participants >= 64)
                {

                    diffrence = number_of_participants - 64;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants where compitition_id=" + comp_id + "   ORDER BY user_id DESC)";
                    number_of_participants = number_of_participants - diffrence;

                }
                else if (rt1 != null && number_of_participants >= 32)
                {

                    diffrence = number_of_participants - 32;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants where compitition_id=" + comp_id + "  ORDER BY user_id DESC)";
                    number_of_participants = number_of_participants - diffrence;

                }
                else if (rt1 != null && number_of_participants >= 16)
                {

                    diffrence = number_of_participants - 16;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants where compitition_id=" + comp_id + "  ORDER BY user_id DESC)";
                    number_of_participants = number_of_participants - diffrence;
                    // Send Notification to inform the user that we are at 16th stage

                }
                else if (rt1 != null && number_of_participants >= 8)
                {

                    diffrence = number_of_participants - 8;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants  where compitition_id=" + comp_id + " ORDER BY user_id DESC)";
                    number_of_participants = number_of_participants - diffrence;

                }
                else if (rt1 != null && number_of_participants >= 4)
                {
                    
                    diffrence = number_of_participants - 4;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants where compitition_id=" + comp_id + "  ORDER BY user_id DESC)";
                    number_of_participants = number_of_participants - diffrence;
                    if (number_of_participants == 4)
                    {
                        send_quatror_part(number_of_participants, comp_id);
                    }

                }

                else if (rt1 != null && number_of_participants >= 2)
                {

                    diffrence = number_of_participants - 2;
                    sql = "Delete from Free_Compitition_Participants where user_id in (SELECT TOP " + diffrence + " user_id FROM Free_Compitition_Participants where compitition_id=" + comp_id + "  ORDER BY user_id DESC)";
                    number_of_participants = number_of_participants - diffrence;
                    //Notification to inform the user it is final Round
                   // send_quatror_part(number_of_participants, comp_id);


                }
                else
                {
                    if (rt1 != null && number_of_participants ==1)
                    {
                        // Get The ID 
                        foreach (DataRow row in rt1.Rows)
                        {
                            int user_id = row.Field<int>("user_id");
                            int competition_id = row.Field<int>("compitition_id");
                            sql = "insert into Free_Winners (winner_id,competition_id,date,game) values" +
                                  " (" + user_id + "," + competition_id + ",'"+time+"','Fifa')";
                            sql3 = "delete from Free_Compitition_Participants";
                            SqlCommand cmd1 = new SqlCommand(sql3, dbc.conn);
                            dbc.conn.Open();
                            result5 = cmd1.ExecuteNonQuery();
                            dbc.conn.Close();
                            if (result != 0)
                            {
                                mess = "succ ";

                            }
                            should_continue = 0;

                            // Send notification To winner 
                            notification_firebase.SendToCustomUser send_congrats = new notification_firebase.SendToCustomUser();
                            send_congrats.SendNotification("مبروك فوزك في المسابقة",user_id.ToString());



                        }

                    }
                }

                SqlCommand cmd = new SqlCommand(sql, dbc.conn);

                dbc.conn.Open();
                result = cmd.ExecuteNonQuery();
                dbc.conn.Close();
                if (result != 0)
                {
                    mess = "succ ";

                }
                if(should_continue==1 && number_of_participants >= 2)
                {
                    SqlDataAdapter adapter2 = new SqlDataAdapter("select * from Free_Compitition_Participants where compitition_id= "+comp_id, dbc.conn);

                    adapter2.Fill(rt1);

                    int[] temp_user_id2 = new int[number_of_participants];
                    string[] temp_user_name2 = new string[number_of_participants];

                    Temp_user_id.Clear();
                    foreach (DataRow row in rt1.Rows)
                    {
                        int user_id = row.Field<int>("user_id");
                        Temp_user_id.Add(user_id);
                        string user_name = row.Field<string>("user_name");
                        Temp_user_name.Add(user_name);


                    }
                    temp_user_id2 = Temp_user_id.ToArray();

                    for (int i = 0; i < Temp_user_id.Count; i = i + 2) // you have to debug
                    {
                        sql2 = "insert into Free_Rounds (user_id1,user_id2,date,winner_id,loser_id,compitition_id,user_name1,user_name2) values" +
                " (" + temp_user_id2[i] + "," + temp_user_id2[i + 1] + ",'" + time + "',0,0," + comp_id + ",'" + temp_user_name2[i] + "','" + temp_user_name2[i + 1] + "' )";

                        SqlCommand cmd1 = new SqlCommand(sql2, dbc.conn);
                        dbc.conn.Open();
                        result = cmd1.ExecuteNonQuery();
                        dbc.conn.Close();
                        if (result != 0)
                        {
                            mess2 = "succ ";

                        }


                    }

                }
                


            }




            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = " Data Incorrect ";
            }
        }
        public void DeleteTheLoosers(int comp_id,string time)
        {

            string mess = null;
            int result = 0;
            int result1 = 0;
            int result2 = 0;
            int result5 = 0;

            string sql = "";
            string sql2 = "";
            string sql1 = "";
            string sql5 = "";

            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
         
            try
            {
                //TRY IT 04-05-2020
                string sql15 = "select user_id from Free_Compitition_Participants where winner_flag=0 and compitition_id=" + comp_id;
                SqlDataAdapter adapter15 = new SqlDataAdapter(sql15, dbc.conn);
                DataTable rt15 = new DataTable();

                adapter15.Fill(rt15);


                foreach (DataRow row in rt15.Rows)
                {
                    int user_id = row.Field<int>("user_id");
                    notification_firebase.SendAppolgieToUser send = new notification_firebase.SendAppolgieToUser();
                    notification_firebase.SendAppolgieToUserIOS send2 = new notification_firebase.SendAppolgieToUserIOS();

                    send.SendNotification("لقد خسرت نتمنى لك حظ اوفر في المسابقات القادمة" +
                        "You lost we wish you better next time ", user_id.ToString(), comp_id.ToString());

                    send2.SendNotification("لقد خسرت نتمنى لك حظ اوفر في المسابقات القادمة" +
                        "You lost we wish you better next time ", user_id.ToString(), comp_id.ToString());


                }
                /////////////////////////////////////////////////////////////////////////////////////


                /////////////////////////////////////////////////////////////////////////////////////
                sql1 = "delete from Free_Compitition_Participants where winner_flag=0 and compitition_id=" + comp_id;
                sql5 = "INSERT INTO Paid_Round SELECT * FROM Free_Rounds";
                sql = "delete from Free_Rounds where compitition_id=" + comp_id; 
                sql2 = "update Free_Compitition_Participants set winner_flag=0 where compitition_id=" + comp_id; 



                SqlCommand cmd2 = new SqlCommand(sql1, dbc.conn);
                SqlCommand cmd5 = new SqlCommand(sql5, dbc.conn);

                SqlCommand cmd1 = new SqlCommand(sql, dbc.conn);
                SqlCommand cmd3 = new SqlCommand(sql2, dbc.conn);
                dbc.conn.Open();
                result = cmd2.ExecuteNonQuery();
                result5 = cmd5.ExecuteNonQuery();
                result1 = cmd1.ExecuteNonQuery();
                result2 = cmd3.ExecuteNonQuery();
                


                mess = "delete succ ";
                    dbc.conn.Close();
                    arrange(comp_id,time);

                
               


            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "تأكد من صحة البيانات ";
            }
          
        }
        public void send_appolgy(int number, int comp_id)
        {
            string sql = "SELECT TOP " + number + " user_id FROM Free_Compitition_Participants  where compitition_id=" + comp_id + "  ORDER BY user_id DESC";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql, dbc.conn);
            DataTable rt12 = new DataTable();

            adapter2.Fill(rt12);


            foreach (DataRow row in rt12.Rows)
            {
                int user_id = row.Field<int>("user_id");
                notification_firebase.SendAppolgieToUser send = new notification_firebase.SendAppolgieToUser();
                notification_firebase.SendAppolgieToUserIOS send2 = new notification_firebase.SendAppolgieToUserIOS();

                send.SendNotification("نعتذر تم استبعادك من المسابقة بسبب عدم توفر اماكن نعتذر للازعاج " +
                    "We apologize, you were excluded from the competition because of the lack of places, we apologize for the inconvenience ", user_id.ToString(), comp_id.ToString());

                send2.SendNotification("نعتذر تم استبعادك من المسابقة بسبب عدم توفر اماكن نعتذر للازعاج " +
                    "We apologize, you were excluded from the competition because", user_id.ToString(), comp_id.ToString());


            }
        }
        public void send_quatror_part(int number, int comp_id)
        {
            string sql = "SELECT user_id FROM Free_Compitition_Participants  where compitition_id=" + comp_id + "  ORDER BY user_id DESC";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql, dbc.conn);
            DataTable rt12 = new DataTable();

            adapter2.Fill(rt12);


            foreach (DataRow row in rt12.Rows)
            {
                int user_id = row.Field<int>("user_id");
                notification_firebase.SendToCustomUser send = new notification_firebase.SendToCustomUser();
                send.SendNotification("مبروك وصولك للدور الربع النهائي ", user_id.ToString());


            }
        }
    }
}