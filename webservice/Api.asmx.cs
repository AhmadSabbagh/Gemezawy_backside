using Braintree;
using FifaGame.ASPX;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;

namespace FifaGame.webservice
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(MessageName = "Register_Players", Description = "this method add players")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Register_Players(String username, String phone, String country, String password, String city, String photo , int coins,Double credit , String playstation_id,String email)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            int UserID = 0;
            string Message = "";
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Users where email = '" + email + "' or username= '" + username + "'", dbc.conn);

                adapter.Fill(rt1);
                
                if (rt1 != null && rt1.Rows.Count > 0)
                {
                    mess = "User already exists";
                    result = 0;
                }
                


                else

                {
                    Base64ToImage(photo).Save(Server.MapPath("~/users/" + username + ".jpg"));

                    sql = "insert into Users (username,phone,country,password,city,photo,coins,credit,playstation_id,email,pubg_id) values" +
                           " ('" + username + "','" + phone + "','" + country + "','" + password + "','" + city + "','" + username+".jpg" + "','" + coins + "','" + credit + "','" + playstation_id + "','" + email + "','NoIdYet')";

                   // Image1.ImageUrl = "~/Images/Hello.jpg";

                    SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                    dbc.conn.Open();
                    result = cmd.ExecuteNonQuery();
                    dbc.conn.Close();
                    if (result != 0)
                    {
                        mess = "add succ";
                        SqlDataReader reader;

                        sql = "select id from Users where email='" + email + "'and password='" + password + "' ";
                        SqlCommand cmd2 = new SqlCommand(sql, dbc.conn);
                        cmd.CommandType = CommandType.Text;

                        dbc.conn.Open();

                        reader = cmd2.ExecuteReader();
                        while (reader.Read())
                        {
                            UserID = reader.GetInt32(0);

                        }
                        if (UserID == 0)
                        {
                            Message = " user name or password is incorrect";
                        }
                        else
                            Message = "login succ";
                        reader.Close();

                        dbc.conn.Close();

                    }
                  

                }



            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = " Data Incorrect ";
            }
            var jsonData = new
            {
                message = mess,
                user_id=UserID
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }
        [WebMethod(MessageName = "Get_Free_Comp", Description = "this method return Free Competition  ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Get_Free_Competition()
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<FreeComp> offer = new List<FreeComp>();
            string sql = null;

            try
            {
                SqlDataReader reader;

                sql = "select competition_id,competition_name,competition_date ,competition_price from Free_Competition";
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

               dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FreeComp h = new FreeComp();
                    h.comp_name = reader["competition_name"].ToString();
                    h.comp_price = reader["competition_price"].ToString();
                    h.comp_date = reader["competition_date"].ToString();
                    h.comp_id = Convert.ToInt32(reader["competition_id"]);

                    offer.Add(h);
                }

                reader.Close();
               dbc.conn.Close();
            }
            catch (Exception ex)
            {

               dbc.conn.Close();
            }

            /*  var jsonData = new
              {
                  name_hotoffer = name,
                  picture = agent_id
              };*/
            Context.Response.Write(sr.Serialize(offer));
        }
        [WebMethod(MessageName = "Get_Pubg_Free_Competition", Description = "this method return Pubg_Free_Competition  ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Get_Pubg_Free_Competition()
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<FreeComp> offer = new List<FreeComp>();
            string sql = null;

            try
            {
                SqlDataReader reader;

                sql = "select competition_id,competition_name,competition_date ,competition_price from Pubg_Free_Competition";
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FreeComp h = new FreeComp();
                    h.comp_name = reader["competition_name"].ToString();
                    h.comp_price = reader["competition_price"].ToString();
                    h.comp_date = reader["competition_date"].ToString();
                    h.comp_id = Convert.ToInt32(reader["competition_id"]);

                    offer.Add(h);
                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            /*  var jsonData = new
              {
                  name_hotoffer = name,
                  picture = agent_id
              };*/
            Context.Response.Write(sr.Serialize(offer));
        }


        [WebMethod(MessageName = "add_Coins", Description = "this method add Coins")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Add_Coins(int user_id, int coins )
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
        
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            int user_coins = 0;
            int new_coins = 0;
            try
            {
                DataTable rt2 = new DataTable();
                sql = "select coins from Users where id= " + user_id;

                SqlDataAdapter adapter = new SqlDataAdapter(sql, webservice.dbc.conn);

                adapter.Fill(rt2);

                foreach (DataRow row in rt2.Rows)
                {
                    user_coins = row.Field<int>("coins");

                }

                 new_coins = user_coins + coins;



                sql = "update  Users set coins="+ new_coins + " where id="+user_id;

                
             
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
               dbc.conn.Open();
                result = cmd.ExecuteNonQuery();
                dbc.conn.Close();
                if (result != 0)
                {
                    mess = "add succ ";
                
                }

            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess,
                new_coins = new_coins
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Get_Coins_To_Buy", Description = "this method To Get the Coins To buy")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Coins_To_Buy()
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Coins> offer = new List<Coins>();
            string sql = null;

            try
            {
                SqlDataReader reader;

                sql = "select coins_number , price ,id from Coins";
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Coins h = new Coins();
                  
                    h.coins_number = Convert.ToInt32(reader["coins_number"]);
                    h.coins_price = Convert.ToDouble(reader["price"]);
                    h.coins_id= Convert.ToInt32(reader["id"]);


                    offer.Add(h);
                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            /*  var jsonData = new
              {
                  name_hotoffer = name,
                  picture = agent_id
              };*/
            Context.Response.Write(sr.Serialize(offer));
        }

        [WebMethod(MessageName = "Add_Playstation_id", Description = "this method add add id of playstation")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Add_Playstation_id(int user_id, string playstation_id)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            /*  SHA512Managed SHA512 = new SHA512Managed();
              byte[] Hash = System.Text.Encoding.UTF8.GetBytes(password);
              Hash = SHA512.ComputeHash(Hash);
              StringBuilder sb = new StringBuilder();
              foreach (byte b in Hash)
              {
                  sb.Append(b.ToString("x2").ToLower());
              }
             string pass= sb.ToString();*/
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {





                sql = "update Users set playstation_id='" + playstation_id +"' where id=" + user_id;



                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                dbc.conn.Open();
                result = cmd.ExecuteNonQuery();
                dbc.conn.Close();
                if (result != 0)
                {
                    mess = "add succ ";
                 
                }

            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Add_Pubg_id", Description = "this method add add id of Add_Pubg_id")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Add_Pubg_id(int user_id, string pubg_id,string photo)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            /*  SHA512Managed SHA512 = new SHA512Managed();
              byte[] Hash = System.Text.Encoding.UTF8.GetBytes(password);
              Hash = SHA512.ComputeHash(Hash);
              StringBuilder sb = new StringBuilder();
              foreach (byte b in Hash)
              {
                  sb.Append(b.ToString("x2").ToLower());
              }
             string pass= sb.ToString();*/
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {

                if (photo.Equals(""))
                    {

                }
                else
                {
                    long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                    string path = user_id + "_" + milliseconds;
                    Base64ToImage(photo).Save(Server.MapPath("~/pubg_id/" + user_id + ".jpg"));
                }

                sql = "update Users set pubg_id='" + pubg_id + "'where id=" + user_id;



                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                dbc.conn.Open();
                result = cmd.ExecuteNonQuery();
                dbc.conn.Close();
                if (result != 0)
                {
                    mess = "add succ ";

                }

            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

       

        [WebMethod(MessageName = "Subscribe_To_Free_Competition", Description = "this method to      to Free Comp")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Subscribe_To_Free_Competition(int user_id, string compitition_id,string user_name,string play_id)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            /*  SHA512Managed SHA512 = new SHA512Managed();
              byte[] Hash = System.Text.Encoding.UTF8.GetBytes(password);
              Hash = SHA512.ComputeHash(Hash);
              StringBuilder sb = new StringBuilder();
              foreach (byte b in Hash)
              {
                  sb.Append(b.ToString("x2").ToLower());
              }
             string pass= sb.ToString();*/
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter("select * from Free_Compitition_Participants where user_id like '" + user_id + "' "+ "and compitition_id like '"+compitition_id+"'", dbc.conn);

                adapter.Fill(rt1);
                if (rt1 != null && rt1.Rows.Count > 0)
                {
                    mess = "User already exists";
                    result = 0;
                }


                else
                {



                    sql = "insert into Free_Compitition_Participants (user_id,compitition_id,user_name,playstation_id) values(" + user_id + "," + compitition_id + ",'"+user_name+"','"+play_id+"')";




                    SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                    dbc.conn.Open();
                    result = cmd.ExecuteNonQuery();
                    dbc.conn.Close();
                    if (result != 0)
                    {
                        mess = "add succ ";

                    }

                }
            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

       

        [WebMethod(MessageName = "Subscribe_To_Pubg_Free_Competition", Description = "this method to subscribe to Paid Comp")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Subscribe_To_Pubg_Free_Competition(int user_id, string competition_id, string user_name, string pubg_id)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            /*  SHA512Managed SHA512 = new SHA512Managed();
              byte[] Hash = System.Text.Encoding.UTF8.GetBytes(password);
              Hash = SHA512.ComputeHash(Hash);
              StringBuilder sb = new StringBuilder();
              foreach (byte b in Hash)
              {
                  sb.Append(b.ToString("x2").ToLower());
              }
             string pass= sb.ToString();*/
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter("select * from Pubg_Free_Competition_Participants where user_id like '" + user_id + "' " + "and competition_id like '" + competition_id + "'", dbc.conn);

                adapter.Fill(rt1);
                if (rt1 != null && rt1.Rows.Count > 0)
                {
                    mess = "User already exists";
                    result = 0;
                }


                else
                {



                    sql = "insert into Pubg_Free_Competition_Participants (user_id,competition_id,username,pubg_id) values(" + user_id + "," + competition_id + ",'" + user_name + "','" + pubg_id + "')";




                    SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                    dbc.conn.Open();
                    result = cmd.ExecuteNonQuery();
                    dbc.conn.Close();
                    if (result != 0)
                    {
                        mess = "add succ ";

                    }

                }
            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

 

      

        [WebMethod(MessageName = "User_Win_in_Free_Competitions", Description = "this method to Declare that the user has been won")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void User_Win_Free_competition(int user_id,string img,int compitition_id,int competator_id)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            /*  SHA512Managed SHA512 = new SHA512Managed();
              byte[] Hash = System.Text.Encoding.UTF8.GetBytes(password);
              Hash = SHA512.ComputeHash(Hash);
              StringBuilder sb = new StringBuilder();
              foreach (byte b in Hash)
              {
                  sb.Append(b.ToString("x2").ToLower());
              }
             string pass= sb.ToString();*/
            string mess = null;
            int result = 0;
            string sql = "";
            string sql2 = "";
            int id = 0;
            int compet_flag=0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                SqlDataReader reader;
                sql2 = "select winner_flag from Free_Compitition_Participants where user_id=" + competator_id + "and compitition_id ="+compitition_id;
                SqlCommand cmd2 = new SqlCommand(sql2, dbc.conn);
                cmd2.CommandType = CommandType.Text;
                dbc.conn.Open();
                reader = cmd2.ExecuteReader();


                while (reader.Read())
                {
                  compet_flag= Convert.ToInt32(reader["winner_flag"]); 
                }

                reader.Close();
                dbc.conn.Close();
                if (compet_flag == 0)
                {
                    long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                    string path = user_id +"_"+ compitition_id;
                    Base64ToImage(img).Save(Server.MapPath("~/win_proof/" + path + ".jpg"));

                    sql = "update Free_Compitition_Participants set winner_flag=1 ,winner_photo='" + path + ".jpg" + "'  where user_id=" + user_id + "and compitition_id=" + compitition_id  ;
                   
                    SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                    dbc.conn.Open();

                    result = cmd.ExecuteNonQuery();
                    dbc.conn.Close();
                    if (result != 0)
                    {
                        mess = "add succ ";

                    }
                }
                else if(compet_flag==1)
                {
                    mess = "you lost";
                }

            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "User_Win_Pubg_Free_competition", Description = "this method to Declare that the user has been won in PUBG")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void User_Win_Pubg_Free_competition(int user_id, string img, int compitition_id)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            /*  SHA512Managed SHA512 = new SHA512Managed();
              byte[] Hash = System.Text.Encoding.UTF8.GetBytes(password);
              Hash = SHA512.ComputeHash(Hash);
              StringBuilder sb = new StringBuilder();
              foreach (byte b in Hash)
              {
                  sb.Append(b.ToString("x2").ToLower());
              }
             string pass= sb.ToString();*/
            string mess = null;
            int result = 0;
            string sql = "";
            string sql2 = "";
            int id = 0;
            int compet_flag = 0;
           // DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
           

                DataTable rt1 = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter("select * from Pubg_Free_Competition_Participants where winner_flag= 1 and competition_id ="+compitition_id, dbc.conn);

                adapter.Fill(rt1);
                int number_of_participants = rt1.Rows.Count;
                if (number_of_participants <4)
                {
                    long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                    string path = user_id + "_" + compitition_id;
                    Base64ToImage(img).Save(Server.MapPath("~/win_proof_pubg/" + path + ".jpg"));
                    sql = "update Pubg_Free_Competition_Participants set winner_flag=1 ,winner_photo1='" + path + ".jpg" + "'  where user_id=" + user_id + "and competition_id=" + compitition_id  ;

                    SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                    dbc.conn.Open();

                    result = cmd.ExecuteNonQuery();
                    dbc.conn.Close();
                    if (result != 0)
                    {
                        mess = "add succ ";

                    }
                }
                else 
                {
                    mess = "you lost";
                }

            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }



        [WebMethod(MessageName = "User_Complain", Description = "this method to User_Complain")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void User_Complain(int user_id, string phone,string user_name,string complain,int round_id,string img)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            /*  SHA512Managed SHA512 = new SHA512Managed();
              byte[] Hash = System.Text.Encoding.UTF8.GetBytes(password);
              Hash = SHA512.ComputeHash(Hash);
              StringBuilder sb = new StringBuilder();
              foreach (byte b in Hash)
              {
                  sb.Append(b.ToString("x2").ToLower());
              }
             string pass= sb.ToString();*/
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                Random r = new Random();
                int num = r.Next();
                long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                string path = user_name + milliseconds;
                Base64ToImage(img).Save(Server.MapPath("~/complaints/" + path + ".jpg"));

                sql = "insert into Players_Complaints (user_id,user_phone,user_name,complain,round_id,img) values(" + user_id + ",'" + phone + "','" + user_name + "','" + complain + "'," + round_id + ",'" + path + ".jpg" + "');";


                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                dbc.conn.Open();
                result = cmd.ExecuteNonQuery();
                dbc.conn.Close();
                if (result != 0)
                {
                    mess = "add succ ";

                }

            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }


        [WebMethod(MessageName = "User_Pubg_Complain", Description = "this method to User_Pubg_Complain")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void User_Pubg_Complain(int user_id, string phone, string user_name, string complain, int competion_id,string img)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            /*  SHA512Managed SHA512 = new SHA512Managed();
              byte[] Hash = System.Text.Encoding.UTF8.GetBytes(password);
              Hash = SHA512.ComputeHash(Hash);
              StringBuilder sb = new StringBuilder();
              foreach (byte b in Hash)
              {
                  sb.Append(b.ToString("x2").ToLower());
              }
             string pass= sb.ToString();*/
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                Random r = new Random();
                int num = r.Next();
                long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                string path = user_name + milliseconds;
                Base64ToImage(img).Save(Server.MapPath("~/pubg_complaints/" + path + ".jpg"));

                sql = "insert into Players_Pubg_Complaints (user_id,user_phone,user_name,complain,competition_id,img) values (" + user_id + ",'" + phone + "','" + user_name + "','" + complain + "'," + competion_id + ",'" + path + ".jpg" + "');";

                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                dbc.conn.Open();
                result = cmd.ExecuteNonQuery();
                dbc.conn.Close();
                if (result != 0)
                {
                    mess = "add succ ";

                }

            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }


        [WebMethod(MessageName = "Login_Player", Description = "Login_Player  ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Login_Player(string email, string password)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            int UserID = 0;
            string Message = "";
            string sql = "";
            SHA512Managed SHA512 = new SHA512Managed();
            byte[] Hash = System.Text.Encoding.UTF8.GetBytes(password);
            Hash = SHA512.ComputeHash(Hash);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in Hash)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            string pass = sb.ToString();

            try
            {
                SqlDataReader reader;

                sql = "select id from Users where email='" + email + "'and password='" + password + "' ";
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserID = reader.GetInt32(0);

                }
                if (UserID == 0)
                {
                    Message = " user name or password is in correct";
                }
                else
                    Message = "login succ";
                reader.Close();

                dbc.conn.Close();


            }
            catch (Exception ex)
            {
                Message = " cannot access to the data";


            }

            var jsonData = new
            {
                id = UserID,
                message = Message
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Get_Playstation_Id", Description = "this method To Get_Playstation_Id")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Playstation_Id(int user_id)
        {
            Playstation_id h;
            h = new Playstation_id();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Playstation_id> offer = new List<Playstation_id>();
            string sql = null;

            try
            {
                SqlDataReader reader;

                sql = "select playstation_id  from Users where id="+user_id;
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    

                    h.Play_id = Convert.ToInt32(reader["playstation_id"]);


                    offer.Add(h);
                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

              var jsonData = new
              {
                  playstation_id = h.Play_id
              };
            Context.Response.Write(sr.Serialize(jsonData));
        }
        [WebMethod(MessageName = "Get_Pubg_Id", Description = "this method To Get_Pubg_Id")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Pubg_Id(int user_id)
        {
            Playstation_id h;
            h = new Playstation_id();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Playstation_id> offer = new List<Playstation_id>();
            string sql = null;

            try
            {
                SqlDataReader reader;

                sql = "select pubg_id  from Users where id=" + user_id;
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {


                    h.Play_id = Convert.ToInt32(reader["pubg_id"]);


                    offer.Add(h);
                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            var jsonData = new
            {
                pubg_id = h.Play_id
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }
        [WebMethod(MessageName = "Get_Next_Match", Description = "this method To Get_Next_Match")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Next_Match(int user_id)
        {
            Rounds h;
            h = new Rounds();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Rounds> offer = new List<Rounds>();
            List<int> comp_id_temp = new List<int>();
            string sql = null;
            Stripe.StripeConfiguration.ApiKey = "sk_test_Q6ruvADyuHoI5fD20dtkbNBp";
            string temp_name = "";
            string play_id = "";
            

            try
            {
                DataTable rt2 = new DataTable();
                sql = "select username from Users where id like '" + user_id + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, webservice.dbc.conn);

                adapter.Fill(rt2);
                int number_of_participants = rt2.Rows.Count;

            
                    foreach (DataRow row in rt2.Rows)
                    {

                        temp_name = row.Field<string>("username");
                   // play_id = row.Field<string>("playstation_id");


                }




                SqlDataReader reader;

                sql = "select *  from Free_Rounds where user_id1=" + user_id +"or user_id2="+user_id;
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    h.round_id = Convert.ToInt32(reader["round_id"]);
                    h.comp_id = Convert.ToInt32(reader["compitition_id"]);
                 
                    if (user_id== Convert.ToInt32(reader["user_id1"]))
                    {
                        h.competator_id = Convert.ToInt32(reader["user_id2"]);
                        comp_id_temp.Add ( h.competator_id);

                    }
                    else
                    {
                        h.competator_id = Convert.ToInt32(reader["user_id1"]);
                        comp_id_temp.Add(h.competator_id);


                    }
                    if (temp_name.Equals( reader["user_name1"]))
                    {
                        h.competator_name = reader["user_name2"].ToString();
                    }
                    else
                    {
                        h.competator_name = reader["user_name1"].ToString();
                    }

                    h.comp_date = reader["date"].ToString();
              


                    offer.Add(h);
                }


                reader.Close();
                dbc.conn.Close();

               /* for(int i=0;i<comp_id_temp.Count;i++)
                {
                    DataTable rt3 = new DataTable();
                    sql = "select playstation_id  from Users where id like '" + comp_id_temp[i] + "'";

                    SqlDataAdapter adapter2 = new SqlDataAdapter(sql, webservice.dbc.conn);

                    adapter2.Fill(rt3);


                    foreach (DataRow row in rt3.Rows)
                    {

                        h.play_id = row.Field<string>("playstation_id");
                        // play_id = row.Field<string>("playstation_id");


                    }
                }*/
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }
            /*
            var jsonData = new
            {
                playstation_id = h.Play_id
            };
            */
            Context.Response.Write(sr.Serialize(offer));
        }
        /*
        [WebMethod(MessageName = "Get_Stripe", Description = "this method To Get_Stripe")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Stripe(int id)
        {
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            double price;
            long amount1=1;
            string sql = null;
            SqlDataReader reader;
            try
            {
                sql = "select *  from Coins where id=" + id;
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {


                    price = Convert.ToDouble(reader["price"]);
                    amount1 = Convert.ToInt64(price);
                    amount1 = amount1 * 100;

                }
                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            Rounds h;
            h = new Rounds();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Rounds> offer = new List<Rounds>();
            Stripe.StripeConfiguration.ApiKey = "sk_test_Q6ruvADyuHoI5fD20dtkbNBp";
         

            PaymentIntentService service = new PaymentIntentService();
            PaymentIntent paymentIntent = new PaymentIntent();
            PaymentIntentCreateOptions options = new PaymentIntentCreateOptions
            {
                Amount = amount1,
                Currency = "usd",
             
            };
            string client_secret  = service.Create(options).ClientSecret;



            var jsonData = new
            {
                CI = client_secret,
                publish_key = "pk_test_V5Gmvg7Wjt92mWgQFVspvmDk"
            };
            
            Context.Response.Write(sr.Serialize(jsonData));
        }
        [WebMethod(MessageName = "Get_Stripe_comp", Description = "this method To Get_Stripe_competition")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Stripe_comp(int id)
        {
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            double price;
            long amount1 = 1;
            string sql = null;
            SqlDataReader reader;
            try { 
            sql = "select *  from Free_Competition where competition_id=" + id;
            SqlCommand cmd = new SqlCommand(sql, dbc.conn);
            cmd.CommandType = CommandType.Text;

            dbc.conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {


                price = Convert.ToDouble(reader["competition_price"]);
                amount1 = Convert.ToInt64(price);
                amount1 = amount1 * 100;






            }

            reader.Close();
            dbc.conn.Close();
        }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

    Rounds h;
            h = new Rounds();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Rounds> offer = new List<Rounds>();
            Stripe.StripeConfiguration.ApiKey = "sk_test_Q6ruvADyuHoI5fD20dtkbNBp";


            PaymentIntentService service = new PaymentIntentService();
            PaymentIntent paymentIntent = new PaymentIntent();
            PaymentIntentCreateOptions options = new PaymentIntentCreateOptions
            {
                Amount = amount1,
                Currency = "usd",

            };
            string client_secret = service.Create(options).ClientSecret;



            var jsonData = new
            {
                CI = client_secret,
                publish_key = "pk_test_V5Gmvg7Wjt92mWgQFVspvmDk"
            };

            Context.Response.Write(sr.Serialize(jsonData));
        }
        */
        [WebMethod(MessageName = "Record_Payment", Description = "this method to Record_Payment")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        

        public void Record_Payment(int user_id, string client_secret, string payment_id, string payment_method ,
            string status,double payment_amount,string product)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
          
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                sql = "insert into Payments (user_id,client_secret,payment_id,payment_method,status,payment_amount,product) values(" 
                    + user_id + ",'" + client_secret + "','" + payment_id + "','" + payment_method + "','" + status + "'," + payment_amount + ",'" + product + "');";


                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                dbc.conn.Open();
                result = cmd.ExecuteNonQuery();
                dbc.conn.Close();
                if (result != 0)
                {
                    mess = "add succ ";

                }

            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "SubscribtionCheck", Description = "this method add SubscribtionCheck")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void SubscribtionCheck(int user_id,  int comp_id)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Free_Compitition_Participants where user_id like '" + user_id + "' " + "and compitition_id like '" + comp_id + "'", dbc.conn);

                adapter.Fill(rt1);
                if (rt1 != null && rt1.Rows.Count > 0)
                {
                    mess = "exsist";
                    result = 0;
                }
                else if (rt1 != null && rt1.Rows.Count >= 128)
                {
                    mess = "NoMoreSpace";
                }
                else
                {
                    mess = "succ";
                }





            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = " Data Incorrect ";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "PubgSubscribtionCheck", Description = "this method add PubgSubscribtionCheck")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void PubgSubscribtionCheck(int user_id, int comp_id)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Pubg_Free_Competition_Participants where user_id like '" + user_id + "' " + "and competition_id like '" + comp_id + "'", dbc.conn);

                adapter.Fill(rt1);
                if (rt1 != null && rt1.Rows.Count > 0)
                {
                    mess = "exsist";
                    result = 0;
                }
                else if (rt1 != null && rt1.Rows.Count >=100)
                {
                    mess = "NoMoreSpace";
                }
                else
                {
                    mess = "succ";
                }





            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = " Data Incorrect ";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Get_Token", Description = "this method To Get_Token")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Token()
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();

            //Test
            var gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "xvz6jkh7jnpdr7p3",
                PublicKey = "ttvqg4yfg7dwrhfx",
                PrivateKey = "70c7d2362a082af29d5ac77c581c4ef8"
            };
         /*   var gateway = new BraintreeGateway
           {
               Environment = Braintree.Environment.SANDBOX,
               MerchantId = "mfsfxvbxj2xvjkfm",
               PublicKey = "wgrgmmxqrrf8zmz7",
               PrivateKey = "737f921a8d1860384ca3097ab726ba2b"
           };*/
            
           /* var gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.PRODUCTION,
                MerchantId = "c3jskkv6ss9ptqjz",
                PublicKey = "bbcct5zgy9ycjhrt",
                PrivateKey = "cc9544dcf030c6862583361cf3ad934f"
            };*/
            var clientToken = gateway.ClientToken.Generate();



            var jsonData = new
            {
                CI = clientToken,
               // publish_key = "pk_test_V5Gmvg7Wjt92mWgQFVspvmDk"
            };

            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Checkout", Description = "this method To Checkout")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Checkout(string nonceFromTheClient,decimal amount)
        {
            string result1;
            string error1 = "";
            string trans_id="0";
            JavaScriptSerializer sr = new JavaScriptSerializer();
            //Test
            var gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "xvz6jkh7jnpdr7p3",
                PublicKey = "ttvqg4yfg7dwrhfx",
                PrivateKey = "70c7d2362a082af29d5ac77c581c4ef8"
            };
            /*  var gateway = new BraintreeGateway
              {
                  Environment = Braintree.Environment.SANDBOX,
                  MerchantId = "mfsfxvbxj2xvjkfm",
                  PublicKey = "wgrgmmxqrrf8zmz7",
                  PrivateKey = "737f921a8d1860384ca3097ab726ba2b"
              };*/
            /*var gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.PRODUCTION,
                MerchantId = "c3jskkv6ss9ptqjz",
                PublicKey = "bbcct5zgy9ycjhrt",
                PrivateKey = "cc9544dcf030c6862583361cf3ad934f"
            };*/

            TransactionRequest request = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = nonceFromTheClient,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                Transaction transaction = result.Target;
                Console.WriteLine("Success!: " + transaction.Id);
                result1 = "succ";
                trans_id = transaction.Id;
            }
            else if (result.Transaction != null)
            {
                Transaction transaction = result.Transaction;
                Console.WriteLine("Error processing transaction:");
                Console.WriteLine("  Status: " + transaction.Status);
                Console.WriteLine("  Code: " + transaction.ProcessorResponseCode);
                Console.WriteLine("  Text: " + transaction.ProcessorResponseText);
                result1 = "  Error: " + transaction.ProcessorResponseText;
            }
            else
            {
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    Console.WriteLine("Attribute: " + error.Attribute);
                    Console.WriteLine("  Code: " + error.Code);
                    Console.WriteLine("  Message: " + error.Message);
                    error1 = "Error is : " +error.Message;
                }
                result1 = "  EDRROR: " + error1;
            }


            var jsonData = new
            {
                // CI = clientToken,
                // publish_key = "pk_test_V5Gmvg7Wjt92mWgQFVspvmDk"
                msg = result1,
                tr_id = trans_id,
            };

            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Get_My_Comp", Description = "this method To Get_My_Comp")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_My_Comp(int user_id)
        {
            FreeComp h = new FreeComp();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<FreeComp> offer = new List<FreeComp>();
            string sql = null;
            string sql1 = null;

            try
            {
                SqlDataReader reader;
                
                sql = "select compitition_id  from Free_Compitition_Participants where user_id=" + user_id;
                sql1 = "select competition_id,competition_name,competition_date ,competition_price from Free_Competition where competition_id in ("+sql+")";

                SqlCommand cmd = new SqlCommand(sql1, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    h.comp_name = reader["competition_name"].ToString();
                    h.comp_price = reader["competition_price"].ToString();
                    h.comp_date = reader["competition_date"].ToString();
                    h.comp_id = Convert.ToInt32(reader["competition_id"]);

                    offer.Add(h);
                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            var jsonData = new
            {
              //  comp_id = h.Play_id
            };
            Context.Response.Write(sr.Serialize(offer));
        }



        [WebMethod(MessageName = "Get_My_Pubg_Comp", Description = "this method To Get_My_Pubg_Comp")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_My_Pubg_Comp(int user_id)
        {
            FreeComp h = new FreeComp();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<FreeComp> offer = new List<FreeComp>();
            string sql = null;
            string sql1 = null;

            try
            {
                SqlDataReader reader;

                sql = "select competition_id  from Pubg_Free_Competition_Participants where user_id=" + user_id;
                sql1 = "select competition_id,competition_name,competition_date ,competition_price from Pubg_Free_Competition where competition_id in (" + sql + ")";

                SqlCommand cmd = new SqlCommand(sql1, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    h.comp_name = reader["competition_name"].ToString();
                    h.comp_price = reader["competition_price"].ToString();
                    h.comp_date = reader["competition_date"].ToString();
                    h.comp_id = Convert.ToInt32(reader["competition_id"]);

                    offer.Add(h);
                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            var jsonData = new
            {
                //  comp_id = h.Play_id
            };
            Context.Response.Write(sr.Serialize(offer));
        }


        [WebMethod(MessageName = "Get_Competition_details", Description = "this method return Get_Competition_details  ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Get_Competition_details(int id)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<FreeComp> offer = new List<FreeComp>();
            string sql = null;

            try
            {
                SqlDataReader reader;

                sql = "select competition_id,competition_name,competition_date ,competition_price from Free_Competition where competition_id="+id;
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FreeComp h = new FreeComp();
                    h.comp_name = reader["competition_name"].ToString();
                    h.comp_price = reader["competition_price"].ToString();
                    h.comp_date = reader["competition_date"].ToString();
                    h.comp_id = Convert.ToInt32(reader["competition_id"]);

                    offer.Add(h);
                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            /*  var jsonData = new
              {
                  name_hotoffer = name,
                  picture = agent_id
              };*/
            Context.Response.Write(sr.Serialize(offer));
        }

        [WebMethod(MessageName = "Get_Player_details", Description = "this method To Get_Player_details")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Player_details(int user_id)
        {
            Users h = new Users();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Users> offer = new List<Users>();
            string sql = null;
            string sql1 = null;

            try
            {
                SqlDataReader reader;

                sql = "select *  from Users where id=" + user_id;

                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    h.username = reader["username"].ToString();
                    h.phone = reader["phone"].ToString();
                    h.photo = reader["photo"].ToString();
                    h.email= reader["email"].ToString();
                    h.playstation_id = reader["playstation_id"].ToString();
                    h.pubg_id = reader["pubg_id"].ToString();


                    offer.Add(h);
                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            var jsonData = new
            {
                username = h.username,
                phone=h.phone,
                photo=h.photo,
                playstation_id=h.playstation_id,
                email=h.email,
                pubg_id=h.pubg_id
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Get_Player_Coins", Description = "this method To Get_Player_Coins")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Player_Coins(int user_id)
        {
            Users h = new Users();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Users> offer = new List<Users>();
            string sql = null;
            string sql1 = null;

            try
            {
                SqlDataReader reader;

                sql = "select coins  from Users where id=" + user_id;

                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    h.coins = Convert.ToInt32(reader["coins"]);
                  

                    offer.Add(h);
                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            var jsonData = new
            {
                coins = h.coins,
            
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }


        [WebMethod(MessageName = "Get_Version", Description = "this method To Get_Version")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Version()
        {
            Version h = new Version();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Version> offer = new List<Version>();
            string sql = null;
            string sql1 = null;

            try
            {
                SqlDataReader reader;

                sql = "select *  from App_version";

                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    h.android = Convert.ToInt32(reader["version_number_android"]);
                    h.ios = Convert.ToInt32(reader["version_number_ios"]);
                   


                    offer.Add(h);
                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            var jsonData = new
            {
                android_ver = h.android,
                ios_ver = h.ios,


            };
            Context.Response.Write(sr.Serialize(jsonData));
        }


        [WebMethod(MessageName = "Get_Payment_info", Description = "this method To Get_Payment_info")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_Payment_info(int user_id)
        {
            Users h = new Users();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Users> offer = new List<Users>();
            string sql = null;
            string sql1 = null;
            string email = "";
                string phone = "";
               

            try
            {
                SqlDataReader reader;

                sql = "select payment_info,phone  from Recieved_Payment_info where user_id=" + user_id;

                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    email = reader["payment_info"].ToString();
                    phone = reader["phone"].ToString();
                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            var jsonData = new
            {
                payment_email = email,
                payment_phone= phone
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Get_competator_play_id", Description = "this method To Get_competator_play_id")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get_competator_play_id(int user_id)
        {
            Users h = new Users();
            JavaScriptSerializer sr = new JavaScriptSerializer();
            List<Users> offer = new List<Users>();
            string sql = null;
            string sql1 = null;
            string play_id = "" ;
            string phone = "";
            string photo = "";


            try
            {
                SqlDataReader reader;

                sql = "select playstation_id,phone,photo from Users where id=" + user_id;

                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    play_id = reader["playstation_id"].ToString();
                    phone = reader["phone"].ToString();
                    photo = reader["photo"].ToString();

                }

                reader.Close();
                dbc.conn.Close();
            }
            catch (Exception ex)
            {

                dbc.conn.Close();
            }

            var jsonData = new
            {
                playstation_id = play_id,
                user_phone = phone,
                user_photo=photo
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Update_User_Profile", Description = "this method Update_User_Profile")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]



        public void Update_User_Profile(int user_id, string username,string phone,string img,string email)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {



                if(img.Equals(""))
                {

                }
                else
                {
                    Base64ToImage(img).Save(Server.MapPath("~/users/" + username + ".jpg"));

                }

                sql = "update  Users set phone='"+phone+"',photo='"+ username + ".jpg" + "',email='" + email + "' where id=" + user_id;



                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                dbc.conn.Open();
                result = cmd.ExecuteNonQuery();
                dbc.conn.Close();
                if (result != 0)
                {
                    mess = "add succ ";

                }

            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Update_Payment_info", Description = "this method Update_Payment_info")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Update_Payment_info(int user_id, string email,string phone)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();

            try
            {
                DataTable rt2 = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter("select * from Recieved_Payment_info where user_id=" + user_id,dbc.conn);

                adapter.Fill(rt2);
                int number_of_participants = rt2.Rows.Count;
                if (number_of_participants > 0)
                {
                    sql = "update  Recieved_Payment_info set payment_info='" + email + "',phone='" + phone + "' where user_id=" + user_id;

                 
                }
                else if(number_of_participants==0)
                {
                    sql = "insert into  Recieved_Payment_info (payment_info,user_id,phone) values ('" + email + "' , " + user_id + ",'" + phone + "')";

                }
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                dbc.conn.Open();
                result = cmd.ExecuteNonQuery();
                dbc.conn.Close();
                if (result != 0)
                {
                    mess = "add succ ";

                }

            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "ChangePass", Description = "this method ChangePass")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void ChangePass(int user_id, string oldpass, string newpass)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            int continue1= 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            try
            {
                DataTable rt2 = new DataTable();
                sql = "select id from Users where id=" + user_id + " and password='" + oldpass + "' ";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, webservice.dbc.conn);

                adapter.Fill(rt2);
                int number_of_participants = rt2.Rows.Count;

                if (number_of_participants == 0)
                {
                    mess = " old password is incorrect";
                    
                }
                else
                {
                  
                    continue1 = 1;
                }
                  

                dbc.conn.Close();


                if (continue1 == 1)
                {
                    sql = "update  Users set password=" + newpass +  " where id=" + user_id;



                    SqlCommand cmd1 = new SqlCommand(sql, dbc.conn);
                    dbc.conn.Open();
                    result = cmd1.ExecuteNonQuery();
                    dbc.conn.Close();
                    if (result != 0)
                    {
                        mess = "succ";

                    }
                }

            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }
        [WebMethod(MessageName = "ForgetPassword", Description = "this method ForgetPassword")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void ForgetPassword(string email)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            // string user_password1 = pass;
            //  string decoded_pass1 = decode(user_password1.ToString());
            /*string decoded_pass1 = decode(password1.ToString());
            string form1 = getform();
            string resultTEST = form1 + decoded_pass1;
            getorginal(resultTEST);*/
            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            int continue1 = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            int user_password=0;

            try
            {
                DataTable rt2 = new DataTable();
                sql = "select id from Users where email like '" + email + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, webservice.dbc.conn);

                adapter.Fill(rt2);
                int number_of_participants = rt2.Rows.Count;

                if (number_of_participants == 0)
                {
                    mess = " old password is incorrect";

                }
                else
                {

                    continue1 = 1;
                    foreach (DataRow row in rt2.Rows)
                    {
                         user_password = row.Field<int>("id");



                    }


                }


                dbc.conn.Close();

                //    client.Credentials = new System.Net.NetworkCredential("nasrohoms57@gmail.com", "theyhavexometopassss");

                if (continue1 == 1)
                {
                    try
                    {
                        
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("gemzawy.com@gmail.com", "01853554177Aa");
                        MailMessage msgobj = new MailMessage();
                        msgobj.To.Add(email);
                        msgobj.From = new MailAddress("gemzawy.com@gmail.com");
                        msgobj.Subject = "Rest Password For your Gemzawi Accound";
                       string decoded_pass=decode(user_password.ToString()) ;
                        string form = getform();
                        msgobj.Body = "http://fifaapp-001-site1.ftempurl.com//NewPassword.aspx?unid=" +form+ decoded_pass;
                        client.Send(msgobj);
                        mess = "succ";
                        
                        /*
                        MailMessage msg = new MailMessage();
                        msg.Subject = "Username &password";
                        msg.From = new MailAddress("nasrohoms57@gmail.com");
                        msg.Body = "Hi, <br/>Please check your Login Detailss<br/><br/>Your Username: " + email + "<br/><br/>Your new Password: " +user_password + "<br/><br/>";
                        msg.To.Add(new MailAddress(email));
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.googlemail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = true;
                        NetworkCredential nc = new NetworkCredential("nasrohoms57@gmail.com", "theyhavexometopassss");
                        smtp.Credentials = nc;
                        smtp.Send(msg);
                        SHA512Managed SHA512 = new SHA512Managed();
                        byte[] Hash3 = System.Text.Encoding.UTF8.GetBytes("123456");
                        Hash3 = SHA512.ComputeHash(Hash3);
                        StringBuilder sb2 = new StringBuilder();
                        foreach (byte p in Hash3)
                        {
                            sb2.Append(p.ToString("x2").ToLower());
                        }
                        */

                    }
                    catch (Exception ex)
                    {
                        mess = ex.Message;
                    }

                }
                else
                {
                    mess = "Wrong Email";
                }
            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "Payout_Request", Description = "this method Payout_Request")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void Payout_Request(string payment_info,string type,int coins_number,int user_id,string date)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            int continue1 = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            int user_coins = 0;

            try
            {
                DataTable rt2 = new DataTable();
                sql = "select coins from Users where id= " + user_id ;

                SqlDataAdapter adapter = new SqlDataAdapter(sql, webservice.dbc.conn);

                adapter.Fill(rt2);

                foreach (DataRow row in rt2.Rows)
                {
                    user_coins = row.Field<int>("coins");

                }
                if (user_coins < coins_number)
                {
                    mess = "wrong";
                }


                    else
                {

                    sql = "insert into Payment_Request (user_id,type,payment_info,coins_number,date) values(" + user_id + ",'" + type + "','" + payment_info + "'," + coins_number + ",'" + date + "')";
                    SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                    dbc.conn.Open();
                    result = cmd.ExecuteNonQuery();
                    dbc.conn.Close();
                    if (result != 0)
                    {
                        int new_coins = user_coins - coins_number;

                        sql = "update Users set coins = " + new_coins + "where id = " + user_id;
                        SqlCommand cmd2 = new SqlCommand(sql, dbc.conn);
                        dbc.conn.Open();
                        result = cmd2.ExecuteNonQuery();
                        dbc.conn.Close();
                        if(result!=0)
                        {
                            mess = "add succ ";

                        }
                    }

                } 
                
               
            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }

        [WebMethod(MessageName = "SendTestNotification", Description = "this method SendTestNotification")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SendTestNotification()
        {
            notification_firebase.SendToAllIOS send2 = new notification_firebase.SendToAllIOS();
          //  send2.SendNotification("hello");
            notification_firebase.SendToCustomUserIOS send3 = new notification_firebase.SendToCustomUserIOS();
         //   send3.SendNotification("hello", "14");
            notification_firebase.SendToFifaPlayersIOS send4 = new notification_firebase.SendToFifaPlayersIOS();
          //  send4.SendNotification("hello");
            notification_firebase.SendToPubgPlayersIOS send5 = new notification_firebase.SendToPubgPlayersIOS();
          //  send5.SendNotification("hello");
            notification_firebase.TestNewClass send6 = new notification_firebase.TestNewClass();
           // send6.SendNotification("public", "Hi", "Hello");
           // send6.SendNotificationFromFirebaseCloud();
           // send6.SendNotice(2, "/topics/public", "heloo from last trial");
           // send6.ExcutePushNotification();
            send6.SendNotificationAsync("public", "Hi there it is the 6th-77777777", "Please please work");


        }

        [WebMethod(MessageName = "GetPaymentsRequest", Description = "this method GetPaymentsRequest")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void GetPaymentsRequest(int user_id)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            int continue1 = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            List<Requests> offer = new List<Requests>();

            try
            {


                SqlDataReader reader;

                sql = "select *  from Payment_Request where user_id="+user_id;
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Requests h = new Requests();
                    
                    h.date = reader["date"].ToString();
                    h.status = Convert.ToInt32(reader["status"]);
                    h.coins_number = Convert.ToInt32(reader["coins_number"]);
                    h.request_id = Convert.ToInt32(reader["request_id"]);


                    offer.Add(h);
                }

                reader.Close();
                dbc.conn.Close();


            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(offer));
        }

        [WebMethod(MessageName = "PayWithCoins", Description = "this method PayWithCoins")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PayWithCoins(int user_id, int coins_number)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();

            string mess = null;
            int result = 0;
            string sql = "";
            int id = 0;
            int continue1 = 0;
            DataTable rt1 = new DataTable();
            ReturnData rt = new ReturnData();
            int user_coins = 0;

            try
            {
                DataTable rt2 = new DataTable();
                sql = "select coins from Users where id= " + user_id;

                SqlDataAdapter adapter = new SqlDataAdapter(sql, webservice.dbc.conn);

                adapter.Fill(rt2);

                foreach (DataRow row in rt2.Rows)
                {
                    user_coins = row.Field<int>("coins");

                }
                if (user_coins < coins_number)
                {
                    mess = "you don't have enough coins in your wallet";
                }



                else
                {
                    int diffrence = user_coins - coins_number;

                  
                        int new_coins = user_coins - coins_number;

                        sql = "update Users set coins = " + diffrence + "where id = " + user_id;
                        SqlCommand cmd2 = new SqlCommand(sql, dbc.conn);
                        dbc.conn.Open();
                        result = cmd2.ExecuteNonQuery();
                        dbc.conn.Close();
                        if (result != 0)
                        {
                            mess = "add succ ";

                        }
                    

                }


            }
            catch (Exception ex)
            {
                dbc.conn.Close();
                mess = "Data Incorrect";
            }
            var jsonData = new
            {
                message = mess
            };
            Context.Response.Write(sr.Serialize(jsonData));
        }
        public string decode (string number)
        {
           char [] decoded_pass_number= number.ToCharArray();
            char[] decoded_pass_char = new char[decoded_pass_number.Length];
            for (int i = 0; i<decoded_pass_number.Length;i++)
            {
                switch(decoded_pass_number[i])
                {
                    case '1':
                        decoded_pass_char[i] = 'a';
                        break;
                    case '2':
                        decoded_pass_char[i] = 'b';
                        break;
                    case '3':
                        decoded_pass_char[i] = 'c';
                        break;
                    case '4':
                        decoded_pass_char[i] = 'd';
                        break;
                    case '5':
                        decoded_pass_char[i] = 'x';
                        break;
                    case '6':
                        decoded_pass_char[i] = 'y';
                        break;
                    case '7':
                        decoded_pass_char[i] = 'f';
                        break;
                    case '8':
                        decoded_pass_char[i] = 'k';
                        break;
                    case '9':
                        decoded_pass_char[i] = 'r';
                        break;
                    case '0':
                        decoded_pass_char[i] = 's';
                        break;
                }
            }
            string newdecoded_number = new string(decoded_pass_char);
            return newdecoded_number;
        }
        public string getform()
        {
            string[] form = new string[] { "123-1j)4abcd$%&^@!$#","12llp784abcd$%&^f*gj","1234-094abcd$%&^@!$#",
            "1234*784abcd$%&^@!$#","123423m4abcd$%&^@!$#","121qa784abcd$%&^@!$#","123mn(84abcd$%&^@!$#","1234p0o9bcd$%&^@!$#@"};
            Random rnd = new Random();
            int index = rnd.Next(8);
            return form[index];
        }
        public void getorginal(string user_id)
        {
            char[] id = user_id.ToCharArray();
            int length = id.Length;
            int wanted_length = length - 20;
            char[] getorginal = new char[wanted_length];
            for(int i=(length-wanted_length) ; i<id.Length;i++)
            {
                getorginal[i - (length - wanted_length)] = id[i];
            }
            string getorginal1 = new string(getorginal);
            string user_id_decoded = decode2(getorginal1);
            int user_id2 = Convert.ToInt32(user_id_decoded);
        }
        public string decode2(string number)
        {
            char[] decoded_pass_number = number.ToCharArray();
            char[] decoded_pass_char = new char[decoded_pass_number.Length];
            for (int i = 0; i < decoded_pass_number.Length; i++)
            {
                switch (decoded_pass_number[i])
                {
                    case 'a':
                        decoded_pass_char[i] = '1';
                        break;
                    case 'b':
                        decoded_pass_char[i] = '2';
                        break;
                    case 'c':
                        decoded_pass_char[i] = '3';
                        break;
                    case 'd':
                        decoded_pass_char[i] = '4';
                        break;
                    case 'x':
                        decoded_pass_char[i] = '5';
                        break;
                    case 'y':
                        decoded_pass_char[i] = '6';
                        break;
                    case 'f':
                        decoded_pass_char[i] = '7';
                        break;
                    case 'k':
                        decoded_pass_char[i] = '8';
                        break;
                    case 'r':
                        decoded_pass_char[i] = '9';
                        break;
                    case 's':
                        decoded_pass_char[i] = '0';
                        break;
                }
            }
            string newdecoded_number = new string(decoded_pass_char);
            return newdecoded_number;
        }


    }



}


